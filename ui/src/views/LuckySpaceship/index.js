import ErrorScreen from "@components/ErrorScreen"
import LoadingScreen from "@components/ScreenInitial"
import chip from "@services/chip"
import game from "@services/game"
import toast from "@services/toast"
import user from "@services/user"
import { setCurrentUser } from "@stores/actions/session"
import _ from "lodash"
import { useEffect, useState } from "react"
import { Controller, useForm } from "react-hook-form"
import { useDispatch, useSelector } from "react-redux"
import { useAsync, useAsyncFn, useInterval } from "react-use"
import Bet from "./components/Bet"
import Header from "./components/Header"
import Map from "./components/Map"
import Round from "./components/Round"
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from 'yup'
const signalR = require("@microsoft/signalr")

const init = async () => {
  const detail = await game.getLuckySpaceshipDetail()
  const chips = await chip.getList()
  const roundDetail = await game.getRoundDetail(detail.id, detail.currentRoundId)
  const prevRound = await game.getPrevRound(detail.id)
  const setting = await game.getLuckySpaceshipSetting()

  return {
    detail,
    chips,
    roundDetail,
    prevRound,
    setting
  }
}

const DEFAULT_RESULT = _.chunk(Array(20).fill('-'), 2)

const schema = yup.object({
  gameId: yup.string().nullable().required(),
  roundId: yup.string().nullable().required(),
  bet: yup.array().of(yup.object({
    term: yup.string().nullable().required(),
    name: yup.string().nullable().required(),
  }).required('Bạn chưa đặt cược'))
}).required()

const Game = () => {
  const form = useForm({ resolver: yupResolver(schema) })
  const [detail, setDetail] = useState(null)
  const [chips, setChips] = useState(null)
  const [numbers, setNumbers] = useState(DEFAULT_RESULT)
  const [waitingFor, setWaitingFor] = useState(0)
  const [connected, setConnecteStatus] = useState(false)
  const [luckySpaceshipSetting, setLuckySpaceshipSetting] = useState(null)
  const { currentUser } = useSelector(state => state.session)
  const dispatch = useDispatch()

  useEffect(() => {
    return () => {
      window.connection?.stop()
    }
  }, [])

  useInterval(() => {
    if (window.connection?.state !== 'Connected') {
      setConnecteStatus(false)
    }
  }, 1000)

  const renew = async () => {
    const detail = await game.getLuckySpaceshipDetail()
    const chips = await chip.getList()
    const prevRound = await game.getPrevRound(game.getLuckySpaceshipId())
    const setting = await game.getLuckySpaceshipSetting()

    setNumbers(_.get(prevRound, 'extraProperties.blockResults'))
    setWaitingFor(0)

    form.reset({
      bet: [],
      gameId: detail.id,
      roundId: detail.currentRoundId,
      chip: _.head(_.filter(chips.items, e => e.name !== 'custom')),
    })

    setLuckySpaceshipSetting({ ...setting })
    setDetail({ ...detail })
  }

  const registryEvent = connection => {
    connection.off('RenewGame')
    connection.off('Result')
    connection.off('WaitingFor')
    connection.off('GenerateResult')

    connection.on('RenewGame', async () => {
      await renew()
    })

    connection.on('Result', async ({ roundId, gameId }) => {
      const currentUser = await user.getProfile()
      dispatch(setCurrentUser({ ...currentUser }))

      const round = await game.getRoundDetail(gameId, roundId)

      if (round.status === 'Win' || round.status === 'Tier') {
        return toast.sucess('Chúc mừng bạn đã thắng cược')
      }

      if (round.status === 'Lose') {
        return toast.error('Rất tiếc bạn đã thua cược')
      }
    })

    connection.on('WaitingFor', data => {
      setWaitingFor(_.round(data.waitInSeconds))
      form.reset()
    })

    connection.on('GenerateResult', async numbers => {
      setNumbers(numbers)

      const currentUser = await user.getProfile()
      dispatch(setCurrentUser({ ...currentUser }))
    })

    connection.onclose(() => {
      setConnecteStatus(false)
    })

    connection.onreconnected(async connectionId => {
      const { detail, chips, roundDetail, prevRound } = await init()

      setDetail({ ...detail })
      setChips({ ...chips })

      if (prevRound) {
        setNumbers(_.get(prevRound, 'extraProperties.blockResults'))
      }

      if (!roundDetail) {
        form.reset({
          gameId: detail.id,
          roundId: detail.currentRoundId,
          status: 'New',
          chip: _.head(_.filter(chips.items, e => e.name !== 'custom'))
        })
      } else {
        form.reset({
          id: roundDetail.id,
          status: roundDetail.status,
          ...roundDetail.extraProperties
        })
      }

      registryEvent(connection)
      setConnecteStatus(true)
      connection.invoke("JoinGame", game.getLuckySpaceshipId())
    })
  }

  const initSignalR = gameId => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(process.env.REACT_APP_LUCKY_SPACESHIP_SIGNALR_URL)
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: retryContext => {
          return 1000
        }
      })
      .build()

    window.connection = connection

    registryEvent(connection)

    return connection.start()
      .then(() => connection.invoke("JoinGame", gameId))
      .then(() => toast.sucess('Chào mừng đến với Phi thuyền may mắn'))
      .then(() => setConnecteStatus(true))
  }

  const { loading, error } = useAsync(async () => {
    const { detail, chips, roundDetail, prevRound, setting } = await init()

    setDetail({ ...detail })
    setChips({ ...chips })
    setLuckySpaceshipSetting({ ...setting })

    if (prevRound) {
      setNumbers(_.get(prevRound, 'extraProperties.blockResults'))
    }

    await initSignalR(detail.id, currentUser)

    if (!roundDetail) {
      return form.reset({
        gameId: detail.id,
        roundId: detail.currentRoundId,
        status: 'New',
        chip: _.head(_.filter(chips.items, e => e.name !== 'custom'))
      })
    }

    form.reset({ id: roundDetail.id, status: roundDetail.status, ...roundDetail.extraProperties })
  }, [])

  const [{ loading: betLoading }, submitFn] = useAsyncFn(async input => {
    input.bet ??= _.map(input.bet)

    for (const item of input.bet)
      item.betAmount = item.chip.amount

    input.totalBetAmount = _.sumBy(input.bet, 'betAmount')
    input.extraProperties = { ...input }

    await game.createRoundDetail(input.gameId, input.roundId, input)
      .then(roundDetail => {
        form.reset({ id: roundDetail.id, ...roundDetail.extraProperties, status: roundDetail.status })
        toast.sucess('Đặt cược thành công')
        return user.getProfile().then(currentUser => dispatch(setCurrentUser({ ...currentUser })))
      })
      .catch(error => toast.error(error.message))
  })

  const onSubmit = input => submitFn(input)

  if (loading) return <LoadingScreen />
  if (error) return <ErrorScreen />

  return (
    <form className="d-flex flex-fill flex-column game-page lucky-spaceship" onSubmit={form.handleSubmit(onSubmit)}>
      <input type="hidden" {...form.register('gameId')} />
      <input type="hidden" {...form.register('roundId')} />
      <input type="hidden" {...form.register('status')} />

      <Header game={detail} />

      <Round
        game={detail}
        numbers={numbers}
        waitingFor={waitingFor}
        connected={connected}
      />

      <Controller
        name="bet"
        control={form.control}
        render={({ field }) => (
          <Map
            waitingFor={waitingFor}
            allowSelect={!Boolean(form.watch('id'))}
            chipSelected={form.watch('chip')}
            value={field.value}
            onChange={field.onChange}
            game={detail}
            settings={luckySpaceshipSetting}
          />
        )}
      />

      <Controller
        name="chip"
        control={form.control}
        render={({ field }) => (
          <Bet
            bets={_.map(form.watch('bet'))}
            waitingFor={waitingFor}
            allowSubmit={!Boolean(form.watch('id'))}
            loading={betLoading}
            value={field.value}
            onChange={chip => field.onChange(chip)}
            chips={chips}
            onCancel={() => form.reset()}
          />
        )}
      />
    </form>
  )
}

export default Game
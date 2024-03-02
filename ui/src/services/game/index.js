import http from "@services/http"
import _ from "lodash"


const getLuckySpaceshipDetail = () =>
  http.get('/api/gamble/public-game/' + process.env.REACT_APP_LUCKY_SPACESHIP_GAME_ID)

const getLuckySpaceshipId = () =>
  _.trim(process.env.REACT_APP_LUCKY_SPACESHIP_GAME_ID)

const createRoundDetail = (gameId, roundId, input) =>
  http.post(`/api/gamble/game/${gameId}/round/${roundId}/detail`, input)

const getRoundDetail = (gameId, roundId) =>
  http.get(`/api/gamble/game/${gameId}/round/${roundId}/detail`)

const getHistories = gameId =>
  http.get(`/api/gamble/game/${gameId}/histories`)

const getPrevRound = gameId =>
  http.get(`/api/gamble/game/${gameId}/prev-round`)

const getLuckySpaceshipSetting = () =>
  http.get('/api/gamble/public-game/lucky-spaceship-setting')

export default {
  getLuckySpaceshipDetail,
  createRoundDetail,
  getRoundDetail,
  getHistories,
  getLuckySpaceshipId,
  getPrevRound,
  getLuckySpaceshipSetting
}
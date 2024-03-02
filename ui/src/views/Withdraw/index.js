import PageTitle from "@components/PageTitle"
import { useDispatch, useSelector } from "react-redux"
import FrmWithdrawPassword from "./components/FrmWithdrawPassword"
import { useAsync } from "react-use"
import user from "@services/user"
import { setCurrentUser } from "@stores/actions/session"
import LoadingScreen from "@components/ScreenInitial"
import FrmWithdrawMoney from "./components/FrmWithdrawMoney"

const Withdraw = () => {
  const disapatch = useDispatch()
  const { currentUser } = useSelector(state => state.session)

  const { loading } = useAsync(async () => {
    const userProfle = await user.getProfile()
    disapatch(setCurrentUser(userProfle))
  }, [])

  if (!currentUser) return null
  if (loading) return <LoadingScreen />
  return (
    <div className="game-account-withdraw flex-fill d-flex flex-column">
      <div className="container flex-fill">
        <div className="row mt-2 mb-4">
          <PageTitle to="/tai-khoan/thong-tin" title="Rút tiền" />
        </div>
        <>
          {!currentUser.hasWithdrawPassword && <FrmWithdrawPassword />}
          {currentUser.hasWithdrawPassword && <FrmWithdrawMoney />}
        </>

      </div>
    </div>
  )
}

export default Withdraw
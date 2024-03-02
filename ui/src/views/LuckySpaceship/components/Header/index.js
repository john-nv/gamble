import user from "@services/user"
import { setCurrentUser } from "@stores/actions/session"
import numeral from "numeral"
import { useDispatch, useSelector } from "react-redux"
import { Link } from "react-router-dom"

const Header = ({ game }) => {
  const dispatch = useDispatch()
  const { currentUser } = useSelector(state => state.session)

  const refresh = async () => {
    try {
      const profile = await user.getProfile()
      dispatch(setCurrentUser({ ...profile }))
    } catch (error) {

    }
  }

  if (!game) return null
  return (
    <div className="container mt-2 game-header d-flex justify-content-between">
      <Link to="/" className="text-light text-decoration-none"><i className="fa-solid fa-house"></i></Link>
      <div className="ms-2 flex-fill">{game.name}</div>
      <div>Số dư: {numeral(currentUser.accountBalance).format()}$</div>
      <a role="button" className="ms-2 text-light text-decoration-none" onClick={() => refresh()}>
        <i className="fa-solid fa-rotate"></i>
      </a>
    </div>
  )
}

export default Header
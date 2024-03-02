import { useSelector } from "react-redux"
import deposit from '@assets/img/deposit.862cd26ec57e83f225c4.png'
import withdraw from '@assets/img/withdraw.a41c76f1715915566d01.png'
import account from '@assets/img/ac.6e82c2d8f893679d58fb.png'
import { Link } from "react-router-dom"
import numeral from "numeral"

const Button = props => {
  return (
    <Link className="btn-acc d-flex flex-column align-items-center text-decoration-none ms-3" to={props.to}>
      <span className="icon" style={{ backgroundImage: `url(${props.icon})` }}></span>
      <span className="label text-center text-nowrap">{props.label}</span>
    </Link>
  )
}

const Profile = () => {
  const { currentUser } = useSelector(state => state.session)

  return (
    <div className="container account my-2">
      <div className="d-flex align-items-center">
        <div className="flex-grow-1">
          <div>Chào mừng ID: {currentUser.userName}</div>
          <div className="amount text-light">{numeral(currentUser.accountBalance).format()}$</div>
        </div>
        <div className="d-flex justify-content-between">
          <Button icon={deposit} label="Nạp tiền" to="/tai-khoan/nap-tien" />
          <Button icon={withdraw} label="Rút tiền" to="/tai-khoan/rut-tien" />
          <Button icon={account} label="Tài khoản" to="/tai-khoan/thong-tin" />
        </div>
      </div>
    </div>
  )
}

export default Profile
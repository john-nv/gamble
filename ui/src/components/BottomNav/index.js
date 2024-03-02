import { Link } from "react-router-dom"
import home from '@assets/img/home.93b20d6e835f71c043b8.png'
import activity from '@assets/img/activity.8a6a35ba2ac648314ace.png'
import datin from '@assets/img/datin.svg'
import huodong from '@assets/img/huodong.svg'
import my from '@assets/img/my.87c43b85171cb8232892.png'
import support from "@services/support"


const NavItem = props => {
  if (props.to)
    return (
      <Link to={props.to} className="text-decoration-none bottom-nav-item d-flex flex-column align-items-center">
        <span className="icon" style={{ backgroundImage: `url(${props.icon})` }}></span>
        <span className="label text-center" >{props.label}</span>
      </Link>
    )

  return (
    <a className="text-decoration-none bottom-nav-item d-flex flex-column align-items-center" href={props.href}>
      <span className="icon" style={{ backgroundImage: `url(${props.icon})` }}></span>
      <span className="label text-center" >{props.label}</span>
    </a>
  )
}


const BottomNav = () => {
  return (
    <>
      <div style={{ height: 500 }}></div>
      <div className="bottom-nav  py-2">

        <div className="container d-flex justify-content-between">
          <NavItem icon={home} label="Trang chủ" to="/" />
          <NavItem icon={activity} label="Kỷ lục cá cược" to="/tai-khoan/lich-su-dat-cuoc" />
          <NavItem icon={datin} label="Sảnh xổ số" to="/" />
          <NavItem icon={huodong} label="Dịch vụ khách hàng" href={support.getSupportLink()} />
          <NavItem icon={my} label="Tài khoản" to="/tai-khoan/thong-tin" />
        </div>
      </div>
    </>
  )
}

export default BottomNav
import defautAvatar from '@assets/img/default-avatar-icon-of-social-media-user-vector.jpg'
import BottomNav from "@components/BottomNav"
import support from '@services/support'
import numeral from "numeral"
import { useSelector } from "react-redux"
import { Link } from "react-router-dom"

const Account = () => {
  const { currentUser } = useSelector(state => state.session)

  return (
    <div className="game-account d-flex flex-column flex-fill">
      <div className="container">
        <div className="py-2 d-flex justify-content-between options">
          <div className="flex-fill text-start">
            <Link to="/tai-khoan/cai-dat" className="text-light text-decoration-none account-ico"><i className="fa-solid fa-gear"></i></Link>
          </div>
          <div className="flex-fill text-end">
            <a role="button" className="text-light text-decoration-none account-ico" >
              <i className="fa-solid fa-arrow-right-from-bracket"></i>
            </a>
          </div>
        </div>
        <div className="row">
          <div className="col-12 d-flex align-items-center account-info">
            <div className="me-2 avatar">
              <img src={defautAvatar} className="img-fluid rounded-circle" />
            </div>
            <div>
              <div>Chào mừng ID: {currentUser.userName}</div>
              <div>Giá trị may mắn: N/A</div>
            </div>
          </div>
        </div>
        <div className="row mt-3 account-balance section m-0 rounded">
          <div className="col-4 mb-3">
            <div className="text-center rounded py-4">
              <div>Số dư</div>
              <div className="fw-bold">{numeral(currentUser.accountBalance).format()}</div>
            </div>
          </div>
          <div className="col-4 mb-3">
            <div className="text-center rounded py-4">
              <div>Đặt cược</div>
              <div className="fw-bold">0</div>
            </div>
          </div>
          <div className="col-4 mb-3">
            <div className="text-center rounded py-4">
              <div>Lãi và lỗ</div>
              <div className="fw-bold">0</div>
            </div>
          </div>
        </div>
        <div className="row mt-3">
          <div className="col-6">
            <a role="button" className="btn-deposit w-100" href={support.getSupportLink()}>Nạp tiền</a>
          </div>
          <div className="col-6">
            <Link to="/tai-khoan/rut-tien" role="button" className="btn-withdraw w-100" >Rút tiền</Link>
          </div>
        </div>
        <div className="row mt-4">
          <div className="col-12">
            <ul className="menu m-0 p-0">
              <li className="section">
                <Link to="/tai-khoan/lich-su-dat-cuoc" role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Lịch sử đặt cược</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </Link>
              </li>
              <li className="section">
                <Link to="/tai-khoan/lich-su-nap-tien" role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Lịch sử nạp tiền</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </Link>
              </li>
              <li className="section">
                <Link to="/tai-khoan/lich-su-rut-tien" role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Lịch sử rút tiền</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </Link>
              </li>
              <li className="section">
                <Link to="/" role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Hồ sơ tài trợ</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </Link>
              </li>
              <li className="section">
                <Link to="/" role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Kỷ lục cá cược</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </Link>
              </li>
              <li className="section">
                <Link to="/" role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Trung tâm cá nhân</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </Link>
              </li>
              <li className="section">
                <Link to="/" role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Trung tâm đại ly</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </Link>
              </li>
              <li className="section">
                <Link to="/" role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Trung tâm báo cáo</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <BottomNav />
    </div>
  )
}

export default Account
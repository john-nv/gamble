import PageTitle from "@components/PageTitle"
import auth from "@services/auth"
import toast from "@services/toast"
import { Link } from "react-router-dom"

const Setting = () => {
  const handleClickLogout = () => {
    toast.confirm({
      title: 'Cảnh báo',
      text: 'Bạn có muốn đăng xuất?',
      onConfirmed: () => {
        auth.logout()
        window.location.reload()
      }
    })
  }

  return (
    <div className="game-account-setting flex-fill d-flex flex-column">
      <div className="container flex-fill">

        <div className="row mt-2 mb-4">
          <PageTitle to="/tai-khoan/thong-tin" title="Cài đặt" />
        </div>

        <div className="row">
          <div className="col-12">
            <div className="menu-title"><i className="fa-solid fa-shield-halved me-2"></i>Bảo mật tài khoản</div>
            <ul className="menu m-0 p-0">
              <li className="section">
                <a role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Mật khẩu đăng nhập</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </a>
              </li>
              <li className="section">
                <a role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none" >
                  <span>Mật khẩu rút tiền</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </a>
              </li>
              <li className="section">
                <a role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Số điện thoại</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </a>
              </li>
              <li className="section">
                <a role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Câu hỏi bảo mật</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </a>
              </li>
              <li className="section">
                <a role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Hộp thư bảo mật</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </a>
              </li>
              <li className="section">
                <Link to="/tai-khoan/cai-dat/quan-ly-the-ngan-hang" role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Quản lý thẻ ngân hàng</span>
                  <span><i className="fa-solid fa-chevron-right"></i></span>
                </Link>
              </li>
            </ul>
          </div>
        </div>

        <div className="row">
          <div className="col-12">
            <div className="menu-title"><i className="fa-solid fa-cube me-2"></i>Hệ thống</div>
            <ul className="menu m-0 p-0">
              <li className="section">
                <a role="button" className="text-light d-flex justify-content-between align-items-center text-decoration-none">
                  <span>Phiên bản hệ thống</span>
                  <span >0.1.0.2366</span>
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>

      <div className="container my-4">
        <a className="btn-signout text-decoration-none w-100 d-block text-center" onClick={handleClickLogout}>Đăng Xuất</a>
      </div>
    </div>
  )
}

export default Setting
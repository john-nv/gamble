const LoadingScreen = () => {
  return (
    <div className="loading-screen d-flex flex-column flex-fill align-items-center justify-content-center">
      <div className="spinner-border text-light" role="status" style={{ width: '3rem', height: '3rem' }} >
        <span className="visually-hidden">Loading...</span>
      </div>
      <div className="text-light my-3">Đang khởi tạo dữ liệu...</div>
    </div>
  )
}

export default LoadingScreen
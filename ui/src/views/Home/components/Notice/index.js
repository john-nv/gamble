import notice from '@assets/img/bulletin-icon.a996c907273958ac2d65.svg'
import announcement from '@assets/img/more-announcement.e9aba3242f773db1453a.png'

const Notice = () => {
  return (
    <div className="container d-flex notice my-2 align-items-center">
      <div className="icon" style={{ backgroundImage: `url(${notice})` }}></div>
      <div className='message flex-fill px-2'>
        <span className='text'>Chào mừng</span>
      </div>
      <div className='icon-end' style={{ backgroundImage: `url(${announcement})` }}></div>
    </div>
  )
}

export default Notice

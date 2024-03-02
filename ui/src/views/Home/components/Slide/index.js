import rgbanner from '@assets/img/rgbanner.png'
import rgbanner2 from '@assets/img/rgbanner2.png'
import rgbanner3 from '@assets/img/rgbanner3.png'
import rgbanner4 from '@assets/img/rgbanner4.png'

const Slide = () => {
  return (
    <div className="carousel container slide" data-bs-ride="carousel">
      <div className="carousel-inner">
        <div className="carousel-item active">
          <img src={rgbanner} className="d-block w-100 img-fluid rounded" />
        </div>
        <div className="carousel-item">
          <img src={rgbanner2} className="d-block w-100 img-fluid rounded" />
        </div>
        <div className="carousel-item">
          <img src={rgbanner3} className="d-block w-100 img-fluid rounded" />
        </div>
        <div className="carousel-item">
          <img src={rgbanner4} className="d-block w-100 img-fluid rounded" />
        </div>
      </div>
    </div>
  )
}

export default Slide
import { Link } from "react-router-dom"

const PageTitle = ({ to, title }) => (
  <div className="text-light page-title title">
    <Link to={to} className="text-light"><i className="fa-solid fa-chevron-left"></i></Link> {title}
  </div>
)

export default PageTitle
import { Link } from "react-router-dom"
import letter from '@assets/img/letter.a6d96d31aad6d4b972f8.png'

const TopNav = () => {
  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary top-nav">
      <div className="container">
        <Link className="navbar-brand text-danger" to="/">WynnCasino</Link>
        <a role="button" className="envelope text-light text-decoration-none" style={{ backgroundImage: `url(${letter})` }}>
        </a>
      </div>
    </nav>
  )
}

export default TopNav
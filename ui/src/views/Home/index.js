import BottomNav from "@components/BottomNav"
import TopNav from "@components/TopNav"
import user from "@services/user"
import { setCurrentUser } from "@stores/actions/session"
import { useEffect } from "react"
import { useDispatch } from "react-redux"
import Category from "./components/Category"
import Notice from "./components/Notice"
import Profile from "./components/Profile"
import Slide from "./components/Slide"


const Home = () => {
  const dispatch = useDispatch()

  useEffect(() => {
    user.getProfile()
      .then(currentUser => dispatch(setCurrentUser({ ...currentUser })))
  }, [])

  return (
    <div className="home-page d-flex flex-column flex-fill">
      <TopNav />
      <Slide />
      <Notice />
      <Profile />
      <Category />
      {/* <HighLight /> */}

      <BottomNav />
    </ div>
  )
}

export default Home
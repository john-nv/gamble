const classname = require("classname")
import defaultAvatar from '@assets/img/default-avatar-icon-of-social-media-user-vector.jpg'
import { useEffect, useState } from 'react'
import { useSelector } from 'react-redux'

const AvatarImg = ({ className, userId, width, height }) => {
  const globalSettings = useSelector(state => state.globalSettings)
  const [src, setSrc] = useState()

  useEffect(() => {
    if (userId) {
      let imgSrc = `${globalSettings.fileServerUrl}/api/storage/photo/${userId}`

      if (width && height)
        imgSrc = `${imgSrc}/resize/${width}/${height}`

      setSrc(imgSrc)
    }
  }, [userId, width, height])

  const handleOnError = () => {
    setSrc(defaultAvatar)
  }

  return (
    <img
      src={src}
      className={classname(className, 'img-fluid')}
      onError={handleOnError}
    />
  )
}

export default AvatarImg
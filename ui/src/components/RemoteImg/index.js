import image from "@services/image"

const RemoteImg = props => {
  return <img src={image.getRemoteImage(props.src)} {..._.omit(props, ['src', 'h', 'w'])} />
}

export default RemoteImg
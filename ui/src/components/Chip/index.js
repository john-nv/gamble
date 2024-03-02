import image from "@services/image"
import classname from "classname"

const Chip = ({ onClick, chipItem, active }) => {
  if (!chipItem) return null
  return (
    <button
      type="button"
      className={classname("btn btn-link text-decoration-none text-dark fw-bold btn-chip p-0 rounded-circle", { "active": active })}
      onClick={onClick}
      style={{ background: `url(${image.getRemoteImage(chipItem.id)})` }}>
      {chipItem.name}
    </button>
  )
}

export default Chip
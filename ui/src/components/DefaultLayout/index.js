
const DefaultLayout = ({ children }) => {
  return (
    <div className="default-layout d-flex flex-column flex-fill">
      <div className="flex-fill d-flex flex-column"> {children}</div>
    </div>
  )
}

export default DefaultLayout
const { CButton } = require("@coreui/react")

const AppButton = btnProps => {
  return (
    <CButton {...btnProps} disabled={btnProps.loading}>
      {btnProps.loading && 'Loading...'}
      {!btnProps.loading && btnProps.children}
    </CButton>
  )
}

export default AppButton
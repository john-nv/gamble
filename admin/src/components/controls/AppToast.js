import { CToast, CToastBody, CToastHeader } from "@coreui/react"

const AppToast = props => (
  <>
    <CToast animation={false} autohide={false} visible={props.visible}>
      <CToastHeader closeButton>
        {props.header}
      </CToastHeader>
      <CToastBody>{props.body}</CToastBody>
    </CToast>
  </>
)

export default AppToast
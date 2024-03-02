import { CButton, CModal, CModalBody, CModalFooter, CModalHeader, CModalTitle } from "@coreui/react"

const AppModal = ({ renderBody, renderHeader, renderFooter, onClose, visible, size, onSubmit }) => {
  return (
    <CModal
      alignment="center"
      backdrop="static"
      keyboard
      visible={visible}
      onClose={onClose}
      size={size}
    >
      <CModalHeader>
        <CModalTitle>{renderHeader?.()}</CModalTitle>
      </CModalHeader>
      <CModalBody>
        {renderBody?.()}
      </CModalBody>
      <CModalFooter>
        {renderFooter?.()}
        <CButton color="secondary" onClick={onClose}> Đóng </CButton>
      </CModalFooter>
    </CModal>
  )
}

export default AppModal
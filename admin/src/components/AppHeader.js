import { cilMenu } from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import {
  CContainer,
  CHeader,
  CHeaderDivider,
  CHeaderNav,
  CHeaderToggler
} from '@coreui/react'
import { useDispatch, useSelector } from 'react-redux'
import { AppHeaderDropdown } from './header/index'
import { AppBreadcrumb } from './index'

const AppHeader = () => {
  const dispatch = useDispatch()
  const sidebarShow = useSelector((state) => state.sidebarShow)

  return (
    <CHeader position="sticky" className="mb-4">
      <CContainer fluid>
        <CHeaderToggler className="ps-1" onClick={() => dispatch({ type: 'set', sidebarShow: !sidebarShow })} >
          <CIcon icon={cilMenu} size="lg" />
        </CHeaderToggler>

        <CHeaderNav className="d-none d-md-flex me-auto"></CHeaderNav>
        <CHeaderNav></CHeaderNav>

        <CHeaderNav className="ms-3">
          <AppHeaderDropdown />
        </CHeaderNav>

      </CContainer>
      <CHeaderDivider />
      <CContainer fluid>
        <AppBreadcrumb />
      </CContainer>
    </CHeader>
  )
}

export default AppHeader

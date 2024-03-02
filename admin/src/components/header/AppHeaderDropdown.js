import defaultAvatar from '@assets/images/avatars/default-avatar-icon-of-social-media-user-vector.jpg'
import {
  cilLockLocked
} from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import {
  CAvatar,
  CDropdown,
  CDropdownItem,
  CDropdownMenu,
  CDropdownToggle
} from '@coreui/react'
import auth from '@services/auth'
import toast from '@services/toast'

const AppHeaderDropdown = () => {
  const logout = () => {
    return toast.confirm({
      title: 'Bạn muốn đăng xuất?',
      onConfirmed: () => {
        auth.logout()
        location.reload()
      }
    })
  }

  return (
    <CDropdown variant="nav-item">
      <CDropdownToggle placement="bottom-end" className="py-0" caret={false}>
        <CAvatar src={defaultAvatar} size="md" />
      </CDropdownToggle>
      <CDropdownMenu className="pt-0" placement="bottom-end">
        <CDropdownItem role='button' onClick={logout}>
          <CIcon icon={cilLockLocked} className="me-2" />
          Đăng xuất
        </CDropdownItem>
      </CDropdownMenu>
    </CDropdown>
  )
}

export default AppHeaderDropdown

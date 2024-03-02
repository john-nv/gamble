import AppButton from '@components/controls/AppButton'
import { cilLockLocked, cilUser } from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import {
  CCard,
  CCardBody,
  CCardGroup,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CInputGroup,
  CInputGroupText,
  CRow
} from '@coreui/react'
import { yupResolver } from "@hookform/resolvers/yup"
import auth from '@services/auth'
import toast from '@services/toast'
import _ from 'lodash'
import { useEffect } from 'react'
import { useForm, } from "react-hook-form"
import { useNavigate } from 'react-router-dom'
import { useAsyncFn } from 'react-use'
import * as yup from "yup"

const schema = yup
  .object({
    userName: yup.string().nullable().required("Tên đăng nhập trống"),
    password: yup.string().nullable().required("Mật khẩu trống")
  })
  .required()

const Login = () => {
  const navigate = useNavigate()

  const { register, handleSubmit, formState: { errors, dirtyFields } } = useForm({
    resolver: yupResolver(schema),
  })

  useEffect(() => {
    if (auth.isLoggined())
      navigate('/')

  }, [])

  const [{ loading, error }, login] = useAsyncFn(async input => {
    try {
      const { accessToken, profile } = await auth.login(input)
      auth.saveAccessToken(accessToken)
      navigate('/')
    } catch (ex) {
      toast.error(ex.details)
    }
  })

  return (
    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={6}>
            <CCardGroup>
              <CCard className="p-4">
                <CCardBody>
                  <CForm onSubmit={handleSubmit(login)}>
                    <h1 className='my-5 text-center'>Hệ thống quản lý WynnCasino</h1>
                    <CInputGroup className="my-2" >
                      <CInputGroupText>
                        <CIcon icon={cilUser} />
                      </CInputGroupText>
                      <CFormInput
                        {...register('userName')}
                        placeholder="Username"
                        autoComplete="username"
                        invalid={_.has(errors, 'userName.message')}
                        valid={_.get(dirtyFields, 'userName') && !_.has(errors, 'userName.message')}
                        feedbackInvalid={_.get(errors, 'userName.message')}
                      />
                    </CInputGroup>
                    <CInputGroup className="mb-2">
                      <CInputGroupText>
                        <CIcon icon={cilLockLocked} />
                      </CInputGroupText>
                      <CFormInput
                        {...register('password')}
                        invalid={_.has(errors, 'password.message')}
                        valid={_.get(dirtyFields, 'password') && !_.has(errors, 'password.message')}
                        feedbackInvalid={_.get(errors, 'password.message')}
                        type="password"
                        placeholder="Password"
                        autoComplete="current-password" />
                    </CInputGroup>
                    <CRow className='my-5'>
                      <CCol xs={12} className='d-flex justify-content-center'>
                        <AppButton color="primary" className="p-2" type='submit' loading={loading}>
                          Đăng nhập
                        </AppButton>
                      </CCol>
                    </CRow>
                  </CForm>
                </CCardBody>
              </CCard>
            </CCardGroup>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default Login

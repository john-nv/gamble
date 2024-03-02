import { cilPenAlt, cilPlus, cilReload, cilSave, cilTrash, cilX } from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import { CButton, CCard, CCol, CModal, CModalBody, CModalFooter, CModalHeader, CModalTitle, CRow, CSpinner } from '@coreui/react'
import http from '@services/http'
import toast from '@services/toast'
import _ from 'lodash'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import ReactTable from 'react-table-6'
import { useAsyncFn } from 'react-use'
import AppButton from './AppButton'
import { useEffect } from 'react'

const DEFAULT_PAGE_SIZE = 10

const AppCrud = ({ columns, getAllApi, getByIdApi, updateApi, createApi, deleteApi, renderForm, resolver, modalOptions, overrideCreateFn, renderFormFooter, updateLabel, updateIcon, renderFormTitle, saveChangeBtnLabel }) => {
  const [createOrUpdateModalVisible, setCreateOrUpdateModalVisible] = useState(false)
  const createOrUpdateForm = useForm({ resolver })
  const [entityModel, setEntityModel] = useState(null)
  const [maxResultCount, setMaxResultCount] = useState(DEFAULT_PAGE_SIZE)


  const [{ loading: getAllLoading, value: items }, getAllFn] = useAsyncFn((params = null) => http.get(getAllApi, { params }))

  const [{ loading: getByIdLoading }, getByIdFn] = useAsyncFn(id => http.get(_.replace(getByIdApi, ':id', id)))

  const [{ loading: updateLoading }, updateFn] = useAsyncFn((id, input) => http.put(_.replace(updateApi, ':id', id), input))

  const [{ loading: createLoading }, createFn] = useAsyncFn((input) => http.post(createApi, input))

  const [{ }, removeFn] = useAsyncFn(id => http.delete(_.replace(deleteApi, ':id', id)))

  useEffect(() => {
    !createOrUpdateModalVisible && getAllFn()
  }, [createOrUpdateModalVisible])


  const createOrUpdateSubmitHandler = async input => {
    if (input.id && updateApi) {
      try {
        await updateFn(input.id, input)
        toast.sucess('Cập nhật thành công')
        setCreateOrUpdateModalVisible(false)
        getAllFn()
      } catch {
        toast.error('Cập nhật thất bại')
      }

      return
    }

    delete input.id

    if (!overrideCreateFn && createApi) {
      return await createFn(input)
        .then(() => toast.sucess('Tạo thành công'))
        .catch(() => toast.error('Tạo thất bại'))
        .then(() => getAllFn())
        .then(() => setCreateOrUpdateModalVisible(false))

    } else {
      return await overrideCreateFn(input)
        .then(() => toast.sucess('Tạo thành công'))
        .catch(() => toast.error('Tạo thất bại'))
        .then(() => getAllFn())
        .then(() => setCreateOrUpdateModalVisible(false))
    }
  }

  const openFormUpdate = async row => {
    setCreateOrUpdateModalVisible(true)

    getByIdFn(row.id)
      .then(entity => {
        setEntityModel({ ...entity })
        createOrUpdateForm.reset({ ...entity })
      })
  }

  const del = row => {
    return toast.confirm({
      title: `Bạn muốn xoá dữ liệu này`,
      text: `Dữ liệu này sẽ bị xoá vĩnh viễn`,
      confirmButtonText: "Xoá",
      onConfirmed: async () => {
        try {
          await removeFn(row.id)
          toast.sucess('Xoá thành công')
          getAllFn()
          setCreateOrUpdateModalVisible(false)
        } catch {
          toast.error('Xoá thất bại')
        }
      }
    })
  }

  const create = () => {
    setEntityModel(null)
    createOrUpdateForm.reset({})
    setCreateOrUpdateModalVisible(true)
  }

  const onCloseForm = () => {
    setEntityModel(null)
    createOrUpdateForm.reset({})
    setCreateOrUpdateModalVisible(false)
  }

  const optionColumns = [{
    accessor: 'id',
    Header: '#',
    maxWidth: Boolean(_.trim(deleteApi)) ? 180 : 125,
    Cell: props => (
      <div className="d-flex justify-content-between">
        <CButton color="primary" size='sm' onClick={() => openFormUpdate(props.original)} className='d-flex align-items-center'>
          {updateIcon ?? <CIcon icon={cilPenAlt} size="lg" className='me-1' />}
          {updateLabel ?? "Cập nhật"}
        </CButton>
        {Boolean(_.trim(deleteApi)) && (
          <CButton color="danger" size='sm' onClick={() => del(props.original)}>
            <CIcon icon={cilTrash} size="lg" className='me-1' />
            Xoá
          </CButton>
        )}
      </div>
    )
  }]

  return (
    <>
      <CRow className="mb-3" >
        <CCol className="d-flex justify-content-end">
          {Boolean(_.trim(createApi)) && (
            <CButton onClick={create} className='d-flex align-items-center'>
              <CIcon icon={cilPlus} className='me-1' />
              Tạo mới
            </CButton>
          )}
          <CButton className='ms-1' onClick={() => getAllFn()} color='dark'>
            <CIcon icon={cilReload} className='me-2' />
            Làm mới
          </CButton>
        </CCol>
      </CRow>

      <CRow>
        <CCol>
          <CCard>
            <ReactTable
              loading={getAllLoading}
              data={_.map(items?.items)}
              pages={_.ceil(_.get(items, 'totalCount') / maxResultCount)}
              onPageSizeChange={(newPageSize, newPage) => setMaxResultCount(newPageSize)}
              columns={_.concat(optionColumns, columns)}
              defaultPageSize={DEFAULT_PAGE_SIZE}
              manual
              onFetchData={(state, instance) => {
                const input = {
                  maxResultCount: state.pageSize,
                  skipCount: state.page * state.pageSize,
                }

                for (const key of state.filtered) {
                  input[key.id] = key.value
                }

                getAllFn(input)
              }}
            />
          </CCard>
        </CCol>
      </CRow>

      <CModal
        alignment="center"
        backdrop="static"
        keyboard
        visible={createOrUpdateModalVisible}
        onClose={onCloseForm}
        size={_.get(modalOptions, 'size')}
      >
        <CModalHeader>
          <CModalTitle>
            {renderFormTitle ? renderFormTitle(entityModel) : (entityModel?.id ? "Cập nhật" : "Tạo mới")}
          </CModalTitle>
        </CModalHeader>
        <CModalBody>
          <input type='text' hidden {...createOrUpdateForm.register('id')} />
          {getByIdLoading && <CSpinner />}
          {!getByIdLoading && renderForm?.({
            entity: entityModel,
            form: createOrUpdateForm,
            onSubmit: createOrUpdateForm.handleSubmit(createOrUpdateSubmitHandler)
          })}
        </CModalBody>
        <CModalFooter>
          {renderFormFooter?.({
            entity: entityModel,
            reloadFn: getAllFn,
            closeMainFormModal: () => setCreateOrUpdateModalVisible(false)
          })}

          {(updateApi || createApi) && (
            <AppButton
              color="primary"
              className='d-flex align-items-center'
              loading={createLoading || updateLoading}
              onClick={createOrUpdateForm.handleSubmit(createOrUpdateSubmitHandler)}>
              <CIcon icon={cilSave} size="lg" className='me-1' />
              {saveChangeBtnLabel ?? "Lưu"}
            </AppButton>
          )}

          <CButton color="danger" onClick={() => setCreateOrUpdateModalVisible(false)} className='d-flex align-items-center'>
            <CIcon icon={cilX} size="lg" className='me-1' />
            Đóng
          </CButton>
        </CModalFooter>
      </CModal>
    </>
  )
}

export default AppCrud
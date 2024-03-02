import AppButton from "@components/controls/AppButton"
import { cilX } from "@coreui/icons"
import CIcon from "@coreui/icons-react"
import { CCol, CFormInput, CFormLabel, CInputGroup, CRow } from "@coreui/react"
import http from "@services/http"
import BotAvatarMini from "@views/pages/management/ChatBot/components/BotAvatar"
import _ from "lodash"
import { useState } from "react"
import { useAsyncFn } from "react-use"

const BotSelector = props => {
  const [name, setName] = useState(null)

  const [{ value, loading }, searchBot] = useAsyncFn(name => {
    return http.get('/api/chatbot', { params: { name } })
      .then(bots => {
        const results = _.unionBy(_.concat(props.value, bots), '_id')
        console.log({ results });
        props?.onChange(results)
        return bots
      })
  })

  const deleteBot = bot => {
    props?.onChange(_.filter(props.value, e => e._id !== bot._id))
  }

  const onSearch = () => {
    if (!_.some(_.filter(props.value, e => e.name == name)))
      searchBot(name)
  }

  return (
    <>
      <CRow>
        <CCol>
          <CFormLabel htmlFor="txt-bot-name">Danh sách bot</CFormLabel>
          <CInputGroup className="mb-3">
            <CFormInput placeholder="Nhập tên bot" id="txt-bot-name" onChange={event => setName(event.target.value)} />
            <AppButton loading={loading} type="button" color="secondary" variant="outline" onClick={onSearch}>Tìm</AppButton>
          </CInputGroup>
        </CCol>
      </CRow>

      <CRow className="flex-wrap">
        {_.map(props.value, (bot, idx) => (
          <CCol key={idx} xs={4} className="mb-2">
            <div className="d-flex align-items-center justify-content-between">
              <div style={{ maxWidth: 100, overflow: 'hidden', textOverflow: 'ellipsis' }} className="text-nowrap">{bot.name}</div>
              <a href="javascript:void(0)" onClick={() => deleteBot(bot)}><CIcon icon={cilX} /></a>
            </div>
            <BotAvatarMini _id={bot._id} w={150} h={150} />
          </CCol>
        ))}
      </CRow >
    </>
  )
}

export default BotSelector
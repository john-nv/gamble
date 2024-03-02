import AppCrud from "@components/controls/AppCrud"
import FrmCreateOrUpdate from "./components/FrmCreateOrUpdate"
import Resolver from "./components/Resolver"
import Columns from "./components/Columns"

const ChatBotGroup = () => {
  return (
    <AppCrud
      columns={Columns()}
      resolver={Resolver}
      renderForm={formProps => <FrmCreateOrUpdate {...formProps} />}
      getAllApi="/api/chatbot-group"
      createApi="/api/chatbot-group"
      updateApi="/api/chatbot-group/:id"
      getByIdApi="/api/chatbot-group/:id"
      deleteApi="/api/chatbot-group/:id"
    />
  )
}

export default ChatBotGroup
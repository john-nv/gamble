import AppCrud from "@components/controls/AppCrud"
import FrmCreateOrUpdate from "./components/FrmCreateOrUpdate"
import Columns from "./components/Columns"
import Resolver from "./components/Resolver"

const ChatBotContent = () => {
  return (
    <AppCrud
      columns={Columns()}
      resolver={Resolver}
      renderForm={formProps => <FrmCreateOrUpdate {...formProps} />}
      getAllApi="/api/chatbot-content"
      createApi="/api/chatbot-content"
      updateApi="/api/chatbot-content/:id"
      getByIdApi="/api/chatbot-content/:id"
      deleteApi="/api/chatbot-content/:id"
    />
  )
}

export default ChatBotContent
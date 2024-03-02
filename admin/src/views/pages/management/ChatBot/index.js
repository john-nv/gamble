import AppCrud from "@components/controls/AppCrud"
import Columns from "./components/Columns"
import FrmCreateOrUpdate from "./components/FrmCreateOrUpdate"
import Resolver from "./components/Resolver"

const ChatBot = () => {
  return (
    <AppCrud
      columns={Columns()}
      resolver={Resolver}
      renderForm={formProps => <FrmCreateOrUpdate {...formProps} />}
      getAllApi="/api/chatbot"
      getByIdApi="/api/chatbot/:id"
      updateApi="/api/chatbot/:id"
      createApi="/api/chatbot"
      deleteApi="/api/chatbot/:id"
    />
  )
}


export default ChatBot
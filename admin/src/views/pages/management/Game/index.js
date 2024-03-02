import AppCrud from "@components/controls/AppCrud"
import Columns from "./components/Columns"
import FrmCreateOrUpdate from "./components/FrmCreateOrUpdate"
import Resolver from "./components/Resolver"

const ChatBot = () => {
  return (
    <AppCrud
      modalOptions={{
        size: 'lg'
      }}
      columns={Columns()}
      resolver={Resolver}
      renderForm={formProps => <FrmCreateOrUpdate {...formProps} />}
      getAllApi="/api/gamble/game"
      getByIdApi="/api/gamble/game/:id"
      updateApi="/api/gamble/game/:id"
    />
  )
}


export default ChatBot
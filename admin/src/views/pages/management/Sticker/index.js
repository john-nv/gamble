import AppCrud from "@components/controls/AppCrud"
import Columns from "./components/Columns"
import FrmCreateOrUpdate from "./components/FrmCreateOrUpdate"
import Resolver from "./components/Resolver"
import http from "@services/http"
import image from "@services/image"

const User = () => {
  return (
    <AppCrud
      columns={Columns()}
      resolver={Resolver}
      renderForm={formProps => <FrmCreateOrUpdate {...formProps} />}
      getAllApi="/api/sticker"
      getByIdApi="/api/sticker/:id"
      updateApi="/api/sticker/:id"
      createApi="/api/sticker"
      deleteApi="/api/sticker/:id"
    />
  )
}


export default User
import PageTitle from "@components/PageTitle"
import bank from "@services/bank"
import toast from "@services/toast"
import classname from "classname"
import _ from "lodash"
import { useEffect } from "react"
import { Link } from "react-router-dom"
import { useAsync, useAsyncFn } from "react-use"

const BankAccount = () => {
  const [{ loading, value }, getMyBankAccounts] = useAsyncFn(bank.getMyBankAccounts)

  const [{ }, deleteBankAccount] = useAsyncFn(id => {
    return toast.confirm({
      title: 'Cảnh báo',
      text: 'Bạn muốn xoá tài khoản?',
      onConfirmed: async () => {
        await bank.deleteBankAccount(id)
          .then(() => toast.sucess('Xoá thành công'))
          .catch(() => toast.error('Xoá thất bại'))

        await getMyBankAccounts()
      }
    })
  })

  useEffect(() => {
    getMyBankAccounts()
  }, [])

  return (
    <div className="game-bank-account flex-fill d-flex flex-column">
      <div className="container flex-fill">
        <div className="row mt-2 mb-4">
          <PageTitle to="/tai-khoan/cai-dat" title="QL thẻ ngân hàng" />
        </div>
        <div className="row">
          <div className="col-12 warning">
            Vì sự an toàn cho tiền của bạn, nó sẽ tự động bị khóa sau khi rút tiền thành công. Nếu bạn cần sửa đổi nó, vui lòng liên hệ với dịch vụ khách hàng。
          </div>
        </div>
        <div className="row mt-2">
          {_.map(value, item => (
            <div className="col-12 mb-3" key={item.id}>
              <div className="card rounded-3">
                <div className="card-header">{item.bank.shortName} - {item.bank.name}</div>
                <div className="card-body">
                  <h5 className="card-title">Số tài khoản: {item.no}</h5>
                  <p className="card-text mb-2">Tên chủ tài khoản: {item.name}</p>
                  <p className="card-text">Chi nhánh: {!_.isEmpty(item.branch) ? item.branch : 'N/A'}</p>
                  {_.size(value) > 1 && <a role="button" onClick={() => deleteBankAccount(item.id)} className="btn btn-danger text-deconration-none w-100">Xoá</a>}
                </div>
              </div>
            </div>
          ))}
        </div>
        <div className={classname("row", { "mt-2": !_.some(value) })}>
          <div className="col-12">
            <Link to="/tai-khoan/cai-dat/them-the-ngan-hang" role="button" className="btn-add-account">Bấm vào để thêm tài khoản ngân hàng</Link>
          </div>
        </div>
      </div>
    </div>
  )
}

export default BankAccount
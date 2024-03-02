import classname from "classname"
import moment from "moment"
import numeral from "numeral"

const TransactionListItem = ({ item }) => (
  <li className="list-group-item d-flex justify-content-between" >
    <span>{moment(item.creationTime).format("DD/MM/YYYY HH:mm")}</span>
    <span className="flex-fill text-center">{item.typeName}</span>
    <span style={{ width: 100, maxWidth: 100 }} className={classname("text-end", { "text-success": item.amount >= 0, "text-danger": item.amount < 0 })}>
      {item.amount}
    </span>
  </li>
)


export default TransactionListItem
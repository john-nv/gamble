import PageTitle from "@components/PageTitle"
import TransactionListItem from "@components/Transactions/TransactionListItem"
import user from "@services/user"
import _ from "lodash"
import { PaginatedList } from "react-paginated-list"
import { useAsync } from "react-use"

const BettingHistory = () => {
  const { loading, value } = useAsync(() => user.getTransactions({ types: ['Betting', 'Pay'], maxResultCount: 100 }))

  return (
    <div className="game-transaction d-flex flex-column flex-fill">
      <div className="container flex-fill">
        <div className="row mt-2 mb-4">
          <PageTitle to="/tai-khoan/thong-tin" title="Lịch sử cá cược" />
        </div>

        <PaginatedList
          list={_.map(value?.items)}
          isLoading={loading}
          itemsPerPage={10}
          renderList={(list) => (
            <ul className="list-group">
              {_.map(list, (item, id) => {
                return (
                  <TransactionListItem item={item} key={item.id} />
                );
              })}
            </ul>
          )}
        />
      </div>
    </div>
  )
}


export default BettingHistory
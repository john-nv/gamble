import game from "@services/game"
import classname from "classname"
import moment from "moment"
import { useEffect, useState } from "react"
import { useAsyncFn } from "react-use"

const Histories = ({ gameDetail }) => {
  const [expand, setExpand] = useState(false)
  const [value, setValue] = useState({ items: [], totalCount: 0 })

  const [{ loading }, getHistories] = useAsyncFn(gameId => {
    return game.getHistories(gameId)
      .then(result => setValue(result))
  })

  useEffect(() => {
    setValue({ items: [], totalCount: 0 })

    if (gameDetail && expand) {
      getHistories(gameDetail.id)
    }
  }, [expand, gameDetail])

  return (
    <div className="game-histories col-md-12 px-0 px-md-2 d-flex flex-column justify-content-center position-relative">
      <a role="button" onClick={() => setExpand(!expand)} className="btn btn-link text-light" type="button">
        <i className={classname("fa-solid", { "fa-angle-down": !expand, "fa-angle-up": expand })}></i>
      </a>

      {expand && (
        <div className="history-detail w-100 d-flex flex-column align-items-center justify-content-between">
          {loading && (
            <div className="d-flex justify-content-center align-items-center">
              <div className="spinner-border text-light " role="status">
                <span className="visually-hidden">Loading...</span>
              </div>
            </div>
          )}

          {!loading && _.map(value, (item, idx) => {
            return (
              <div className="result-wrapper d-flex flex-column justify-content-center align-items-center mb-2 w-100" key={idx}>
                <div className="time mb-2">{moment(item.creationTime).format('DD/MM/YYYY HH:mm')}</div>
                <div className="d-flex">
                  {_.map(_.get(item, 'extraProperties.blockResults'), (result, idx1) => (
                    <div className="result-item rounded-circle flex-fill" key={idx + idx1}>{result}</div>
                  ))}
                </div>
              </div>
            )
          })}
        </div>
      )}

    </div>
  )
}

export default Histories
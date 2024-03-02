import toast from "@services/toast";
import classname from "classname";
import _ from "lodash";
import numeral from "numeral";

const Map = ({ game, value, onChange, chipSelected, allowSelect, waitingFor, settings }) => {
  const currentValues = _.map(value)

  if (!game) return null

  const handleTermSelect = (term, rule, block) => {
    if (!allowSelect) return

    if (chipSelected.amount <= 0)
      return toast.error('Đặt cược không hợp lệ')

    currentValues.push({ ...term, ...rule, block: block, chip: chipSelected })
    onChange?.(_.filter(currentValues))
  }

  return (
    <div className="container game-map">
      {_.map(_.get(game, 'extraProperties.blocks'), (block, idx) => (
        <div className="row section d-flex align-items-center py-3 m-0 mb-3 rounded" key={idx}>
          <div className="col-md-12 text-center mb-3 block-name">{block.name}</div>
          <div className="row justify-content-center ball-container m-0 px-2">
            {_.chunk(block.selects, 4).map((blockChunk, blockIdx) => {
              return _.map(blockChunk, (item, itemIdx) => {
                const isSelected = _.some(currentValues, e => e.block.id === block.id && e.term === item.term)
                const amount = _.sumBy(_.filter(currentValues, current => current.block.id === block.id && current.term === item.term), 'chip.amount')
                return (
                  <div key={itemIdx + blockIdx} className="col-md-3 col-3 p-1  ball">
                    <button
                      disabled={Boolean(waitingFor)}
                      onClick={() => handleTermSelect(item, _.get(game, ['extraProperties', 'terms', item.term]), block)}
                      className={classname("btn btn-secondary position-relative btn-lg w-100 d-flex flex-column align-items-center p-1", { "selected": isSelected })}
                      type="button"
                    >
                      <span className="label">{item.name}</span>
                      <span className="term">
                        {(_.includes(['long', 'phung'], item.term) && block.id !== '1') ? settings.defaultRate : _.get(game, ['extraProperties', 'terms', item.term, 'rate'])}
                      </span>
                      {isSelected && (
                        <span className="amount selected rounded d-flex flex-column justify-content-center align-items-center">
                          <span className="label">{item.name} - {numeral(amount).format()}$</span>
                          <span className="term">
                            {(_.includes(['long', 'phung'], item.term) && block.id !== '1') ? settings.defaultRate : _.get(game, ['extraProperties', 'terms', item.term, 'rate'])}
                          </span>
                        </span>
                      )}
                    </button>
                  </div>
                )
              })
            })}
          </div>
        </div>
      ))}
    </div>
  )
}

export default Map
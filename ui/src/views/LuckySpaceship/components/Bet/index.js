import Chip from "@components/Chip"
import AppButton from "@components/Controls/AppButton"
import CustomChip from "@components/CustomChip"
import _ from "lodash"
import { useEffect, useState } from "react"

const Bet = ({ chips, onChange, onCancel, value, loading, allowSubmit, waitingFor, bets }) => {
  const [items, setItems] = useState([])

  useEffect(() => {
    if (_.some(chips?.items)) {
      setItems(chips.items)
    }
  }, [chips])

  const handleOnChangeCustomChip = chipSelected => {
    onChange?.(chipSelected)
  }

  if (!allowSubmit) return null
  if (Boolean(waitingFor)) return null

  return (
    <>
      <div style={{ height: 200 }}></div>
      <div className="game-bet d-flex py-2">
        <div className="container flex-fill d-flex justify-content-between">
          <div className="flex-fill d-flex justify-content-between align-items-center">
            <div className="flex-fill d-flex">
              {_.map(items, (item, idx) => {
                if (item.name === 'custom')
                  return <CustomChip chipItem={item} onChange={handleOnChangeCustomChip} chipSelected={value} />

                return <Chip chipItem={item} key={idx} active={value?.id === item.id} onClick={() => onChange?.({ ...item })} />
              })}
            </div>
            <div className=" d-flex justify-content-end">
              <button className="btn btn-danger me-2 btn-sm" disabled={!_.some(bets)} type="reset" onClick={onCancel}>Huỷ</button>
              <AppButton className="btn btn-primary btn-sm" disabled={!_.some(bets)} loading={loading} type="submit">Xác nhận</AppButton>
            </div>
          </div>
        </div>
      </div>
    </>
  )
}

export default Bet
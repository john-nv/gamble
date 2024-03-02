import _ from "lodash"
import { useEffect, useState } from "react"

const CustomChip = ({ chipItem, chipSelected, onChange }) => {
  const [value, setValue] = useState()

  useEffect(() => {
    if (chipSelected) {
      setValue({ ...chipSelected })
    }

  }, [chipSelected])

  const handleOnChange = value => {
    const valueSelected = {
      ...chipItem,
      amount: _.toInteger(value),
      name: 'custom'
    }

    onChange?.(valueSelected)
  }

  if (!chipItem) return null

  return (
    <div className="custom-chip me-1">
      <input
        className="form-control form-control-sm"
        onChange={event => handleOnChange(event.target.value)}
        value={value?.amount}
      />
    </div>
  )
}

export default CustomChip
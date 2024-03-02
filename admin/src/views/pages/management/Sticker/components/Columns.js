import Avatar from "@components/Avatar"

const Columns = () => {
  return [
    {
      accessor: '_id',
      Header: 'Sticker',
      filterable: true,
      Cell: props => {
        return (
          <div className="d-flex align-items-center" style={{ width: 50, height: 50 }}>
            <Avatar _id={props.original._id} className="me-2" />
          </div>
        )
      }
    }
  ]
}


export default Columns
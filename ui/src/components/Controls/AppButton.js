import _ from "lodash"

const AppButton = props => {
  const loading = props.loading
  return (
    <button {..._.omit(props, 'loading')} disabled={loading || props.disabled}>
      {props.loading && (
        <div className="spinner-border spinner-border-sm" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      )}
      {!props.loading && props.children}
    </button>
  )
}

export default AppButton
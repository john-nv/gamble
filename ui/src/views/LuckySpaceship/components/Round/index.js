import _ from "lodash"
import moment from 'moment'
import Countdown from "react-countdown"
import Histories from "../Histories"
import classname from "classname"


const Round = ({ game, onComplete, numbers, waitingFor, connected }) => {
  if (!game) return null

  return (
    <div className="container game-round my-3" key={game.currentRoundId}>
      <div className="row section d-flex align-items-center py-3 m-0 rounded">
        <div className="col">
          <div className="mb-2">Các trận gần đây</div>
          <div className="result-wrapper d-flex flex-wrap current">
            {_.map(numbers, (item, idx) => (
              <div className="result-item rounded-circle mb-1 me-1" key={idx}>{item}</div>
            ))}
          </div>
        </div>

        <div className="col">
          <div className="result-wrapper d-flex flex-column align-items-end">
            <div className={classname("round-code text-end", { "connected": connected })}>
              {waitingFor > 0 && 'Bắt đầu cược sau'}
              {waitingFor <= 0 && game?.currentRound?.code}
            </div>
            <div className="count-down d-flex justify-content-between">
              {waitingFor > 0 && <div className="text-danger">{waitingFor}s</div>}
              {waitingFor <= 0 && (
                <Countdown
                  date={moment(game?.currentRound?.endTime).local()}
                  onComplete={onComplete}
                  renderer={({ hours, minutes, seconds, completed }) => (
                    <>
                      <span>{_.padStart(hours, 2, '0')}</span>
                      <span className="px-2">:</span>
                      <span>{_.padStart(minutes, 2, '0')}</span>
                      <span className="px-2">:</span>
                      <span>{_.padStart(seconds, 2, '0')}</span>
                    </>
                  )}
                />
              )}
            </div>
          </div>
        </div>
        <Histories gameDetail={game} />
      </div>
    </div>
  )
}

export default Round
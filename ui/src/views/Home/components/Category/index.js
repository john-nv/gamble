import tomau from '@assets/img/1.94732245082a337d973a.png'
import duaXe from '@assets/img/26.b99820baf827d5c86613.png'
import veso from '@assets/img/27.b66a27dc46bf56e60c6a.png'
import tocdo from '@assets/img/28.5004283522d159495603.png'
import phithuyen from '@assets/img/32.552f1aa9ef799e732bce.png'
import sentosa from '@assets/img/43.c402a9fb3d3945ac39b8.png'
import sentosa5 from '@assets/img/45.36dd88e459f576fea52e.png'
import o11 from '@assets/img/6.f45183d305b4055e0491.png'
import _ from 'lodash'
import { Link } from 'react-router-dom'

const categories = [
  {
    id: 'lucky-spaceship',
    name: 'PK đua xe',
    icon: duaXe,
    available: true
  },
  {
    id: 'lucky-spaceship',
    name: 'Vé số',
    icon: veso,
    available: true
  },
  {
    id: 'lucky-spaceship',
    name: 'Phi thuyền may mắn',
    icon: phithuyen,
    available: true
  },
  {
    id: 'lucky-spaceship',
    name: 'Luôn tô màu',
    icon: tomau,
    available: true
  },
  {
    id: 'lucky-spaceship',
    name: 'Tốc độ cực cao',
    icon: tocdo,
    available: true
  },
  {
    id: 'lucky-spaceship',
    name: 'Sentosa',
    icon: sentosa,
    available: true
  },
  {
    id: 'lucky-spaceship',
    name: 'Xổ số 5 điểm',
    icon: sentosa5,
    available: true
  },
  {
    id: 'lucky-spaceship',
    name: '11 ô may mắn',
    icon: o11,
    available: true
  }
]

const Category = () => {
  const chunks = _.chunk(categories, 4)
  const chunkSize = _.size(chunks)

  return (
    <div className='container category-container mt-3'>
      <div className='row m-0'>
        {_.map(chunks, (chunk, chunkIdx) => {
          const itemCls = 'col-3 p-3 category-item d-flex flex-column justify-content-between align-items-center text-decoration-none'
          const isFirstChunk = chunkIdx === 0;
          const isLastChunk = chunkIdx === chunkSize - 1
          const childChunkSize = _.size(chunk)

          return _.map(chunk, (item, itemIdx) => {
            const isBTL = isFirstChunk && itemIdx === 0
            const isBTR = isFirstChunk && itemIdx === childChunkSize - 1
            const isBBL = isLastChunk && itemIdx === 0
            const isBBR = isLastChunk && itemIdx === childChunkSize - 1
            const link = item.available ? `/tro-choi/${item.id}` : '/'
            return (
              <Link
                to={link}
                data-btl={isBTL}
                role='button'
                data-btr={isBTR}
                data-bbl={isBBL}
                data-bbr={isBBR}
                className={itemCls}
                key={itemIdx}>
                <div className='label text-center'>{item.name}</div>
                <div className='icon' style={{ background: `url(${item.icon})` }}></div>
              </Link>
            )
          })
        })}
      </div>
    </div>
  )
}

export default Category
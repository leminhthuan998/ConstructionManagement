import React from 'react'
import history from '../../../history';
const Page401 = () => {
  return (
    <div className="" style={{ display: 'flex', justifyContent: 'center', height: window.innerHeight - 190, alignItems: 'center' }}>
      <h1
        style={{color: "red"}}
        className="float-left display-3 mr-4">401</h1>
      <div style={{ display: 'flex', flexDirection: 'column' }}>
        <h4 className="pt-3">Không có quyền truy cập</h4>
        <p className="text-muted float-left">Vui lòng liên hệ quản trị viên !</p>
        <a
          onClick={() => { history.push('/dashboard'); window.location.reload(); }}
          style={{
            fontSize: 15,
            display: 'flex',
            alignItems: 'center',
            color: '#0665f0',
            cursor: 'pointer',
            textDecoration: 'underline'
          }}>
          Trở về trang chủ
                    </a>
      </div>

    </div>
  )
}

export default Page401

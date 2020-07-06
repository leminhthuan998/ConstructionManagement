import { CButton } from '@coreui/react';
import { AgGridReact } from "ag-grid-react";
import { Button, Modal } from 'antd';
import Axios from 'axios';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { onDeleteConfirm } from '../../application/actions/appAction';
import store from '../../AppStore';
import { API_HOP_DONG_DELETE, API_TTMT_DETAIL, API_THANH_PHAN_DAT_DETAIL } from '../../constants/ApiConstant';
import AppUtil from '../../utils/AppUtil';
// import FormUpdate from './form/FormUpdate';
import FormDetail from "./form/FormDetail";
import moment from 'moment';
import _ from 'lodash';


const mapStateToProps = (state) => {
  return {
    onDelete: state.root.onDelete
  };
};

class ThanhPhanDatListView extends Component {
  constructor(props) {
    super(props);
    const me = this
    this.state = {
      rowData: [],
      columnDefs: [
        {
          headerName: "STT",
          valueGetter: "node.rowIndex + 1",
          width: 80
        },
        {
            headerName: "Ngày trộn",
            field: "thongTinMeTron.ngayTron",
            width: 150,
            suppressSizeToFit: true,
            cellRendererFramework: function (params) {
                return moment.utc(_.get(params.data, 'thongTinMeTron.ngayTron')).format("DD/MM/YYYY HH:mm")
            }
        },
        {
            headerName: "Số xe",
            field: "thongTinMeTron.vehicle.serialNumber",
            width: 130,
            suppressSizeToFit: true
        },
        {
            headerName: "Tên Hợp đồng",
            field: "thongTinMeTron.hopDong.tenHopDong",
            minWidth: 160
        },
        {
            headerName: "Loại bê tông",
            field: "thongTinMeTron.mac.macCode",
            minWidth: 160,
        },
        {
            headerName: "Khối lượng",
            field: "thongTinMeTron.khoiLuong",
            minWidth: 120,
        },
        {
            headerName: "Đá 1",
            field: "da1",
            minWidth: 100
        },
        {
            headerName: "Đá 2",
            field: "da2",
            minWidth: 100
        },
        {
          headerName: "Cát 1",
          field: "cat1",
          width: 150,
          suppressSizeToFit: true
        },
        {
          headerName: "Cát 2",
          field: "cat2",
          width: 150,
          suppressSizeToFit: true
        },
        {
          headerName: "Xi măng 1",
          field: "xiMang1",
          minWidth: 120,
        },
        {
          headerName: "Xi măng 2",
          field: "xiMang2",
          minWidth: 120,
        },
        {
          headerName: "Tro bay",
          field: "troBay",
          minWidth: 120,
        },
        {
          headerName: "Nước",
          field: "nuoc",
          minWidth: 120,
        },
        {
          headerName: "Phụ gia 1",
          field: "phuGia1",
          minWidth: 120,
        },
        {
          headerName: "Phụ gia 2",
          field: "phuGia2",
          minWidth: 120,
        },
        {
          headerName: "Hành động",
          field: "action",
          minWidth: 250,
          cellRendererFramework: function (params) {
            return <div style={{ display: 'flex', alignItems: 'center' }}>
              <button onClick={() => me.openFormDetail(params.data)}
                style={{ height: 30, marginRight: 5, display: 'flex', alignItems: 'center' }} type="button"
                className="btn btn-info">Chi tiết
              </button>
              {/* <button onClick={() => me.openFormEdit(params.data)}
                style={{ height: 30, marginRight: 5, display: 'flex', alignItems: 'center' }} type="button"
                className="btn btn-success">Chỉnh sửa
              </button>
              <button onClick={() => me.onDelete(params.data)}
                style={{ height: 30, display: 'flex', alignItems: 'center' }} type="button"
                className="btn btn-danger">Xóa
              </button> */}
            </div>
          }
        },
      ],
      visible: false,
      visibleEdit: false,
      onDelete: false,
      rowSelect: {},
      visibleCreate: false,
      dataMeTron: []
    }
    this.gridApi = ''
  }

  componentDidMount(){
    Axios.get(AppUtil.GLOBAL_API_PATH + API_TTMT_DETAIL)
        .then(res => {
        console.log("TPDAT -> componentDidMount -> res", res)
          const {data} = res;
          if (data.success) {
            this.setState({
                dataMeTron: data.result
            })
          }
        })
        .catch(() => {
          AppUtil.ToastError();
        })
        .finally(() => {
        });
  }



  componentWillReceiveProps(nextProps) {
    if (nextProps.onDelete !== this.state.onDelete) {
      if (nextProps.onDelete === true) {
        this.onConfirmDelete(this.state.rowSelect)
      }
    }
  }

  //detail
  openFormDetail(data) {
    this.setState({
      visible: true,
      rowSelect: data,
    });
  }

  handleOk = e => {
    this.setState({
      visible: false,
    });
  };

  handleCancel = e => {
    this.setState({
      visible: false,
    });
  };

  //edit
  openFormEdit(data) {
    this.setState({
      visibleEdit: true,
      rowSelect: data,
    });
  }


  handleOkEdit = e => {
    // this.formUpdate.onFinish()
    this.setState({
      visibleEdit: false,
    });
  };

  handleCancelEdit = e => {
    this.setState({
      visibleEdit: false,
    });
  };

  //create
  openFormCreate() {
    this.setState({
      visibleCreate: true,
    });
  }

  handleOkCreate = e => {
    // this.formUpdate.onFinish()
    this.setState({
      visibleCreate: false,
    });
  };

  handleCancelCreate = e => {
    this.setState({
      visibleCreate: false,
    });
  };


  onDelete(data) {
    AppUtil.showConfirm()
    this.setState({
      rowSelect: data
    })
  }

  onConfirmDelete(data) {
    const params = {}
    params['id'] = data.id
    Axios.post(AppUtil.GLOBAL_API_PATH + API_HOP_DONG_DELETE, null, {
      params
    })
      .then(res => {
        const { data } = res;
        if (data.success) {
          AppUtil.ToastSuccess('Xóa dữ liệu thành công!');
          this.loadData()
        }
      })
      .catch(() => {
        AppUtil.ToastError();
      })
      .finally(() => {
        store.dispatch(onDeleteConfirm(false))
      });
  }


  loadData() {
    this.gridApi && this.gridApi.showLoadingOverlay();
    Axios.get(AppUtil.GLOBAL_API_PATH + API_THANH_PHAN_DAT_DETAIL)
      .then(res => {
      console.log("CapPhoiListView -> loadData -> res", res)
        const { data } = res;
        if (data.success) {
          this.setState({
            rowData: data.result 
          })
        }
      })
      .catch(() => {
        AppUtil.ToastError();
      })
      .finally(() => {
        this.gridApi && this.gridApi.hideOverlay();
      });
  }

  onGridReady = params => {
    this.gridApi = params.api;
    this.gridApi.sizeColumnsToFit();
    this.loadData()
  };

  render() {
    return (
      <div className="ag-theme-alpine" style={{
        height: window.innerHeight - 180,
        alignItems: "stretch",
        display: "flex",
        flexDirection: "column",
        paddingTop: 0, boxShadow: 'unset',
        marginTop: -15
      }}>
        <div style={{ display: 'flex', justifyContent: 'flex-end', marginBottom: 5 }}>
          <CButton color="primary" onClick={() => this.openFormCreate()}>+ Tạo mới</CButton>
        </div>
        <AgGridReact
          columnDefs={this.state.columnDefs}
          enableColResize={true}
          rowData={this.state.rowData}
          enableFilter={true}
          enableSorting={true}
          onFilterModified={(...a) => console.log("onFilterModified", ...a)}
          onFilterChanged={(...a) => console.log("onFilterChanged", ...a)}
          onGridReady={this.onGridReady}
        />
        <Modal
          title="Chi tiết"
          visible={this.state.visible}
          onOk={this.handleOk}
          onCancel={this.handleCancel}
          width={'50%'}
          footer={[
            <Button key="back" size="large" type="danger" onClick={this.handleCancel}>Đóng</Button>,
          ]}
        >
          <FormDetail data={this.state.rowSelect} />
        </Modal>
        <Modal
          title="Chỉnh sửa"
          visible={this.state.visibleEdit}
          onOk={this.handleOkEdit}
          onCancel={this.handleCancelEdit}
          width={'50%'}
          footer={[
            <Button key="submit" type="primary" size="large" onClick={this.handleOkEdit}>
              Submit
            </Button>,
            <Button key="back" size="large" type="danger" onClick={this.handleCancelEdit}>Đóng</Button>,

          ]}
        >
          {/* <FormUpdate ref={c => this.formUpdate = c} data={this.state.rowSelect}  dataMac={this.state.dataMac} loadData={() => this.loadData()} /> */}
        </Modal>
        <Modal
          title="Tạo mới"
          visible={this.state.visibleCreate}
          onOk={this.handleOkCreate}
          onCancel={this.handleCancelCreate}
          width={'50%'}
          footer={[
            <Button key="submit" type="primary" size="large" onClick={this.handleOkCreate}>
              Submit
            </Button>,
            <Button key="back" size="large" type="danger" onClick={this.handleCancelCreate}>Đóng</Button>,

          ]}
        >
          {/* <FormUpdate create ref={c => this.formUpdate = c} data={this.state.rowSelect} dataMac={this.state.dataMac}
            loadData={() => this.loadData()} /> */}
        </Modal>
      </div>
    );
  }
}

export default connect(mapStateToProps)(ThanhPhanDatListView);

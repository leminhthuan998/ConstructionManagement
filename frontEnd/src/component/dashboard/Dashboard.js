import { DatePicker, Form, Select } from 'antd';
import Axios from 'axios';
import _ from 'lodash';
import moment from 'moment';
import React, { Component } from 'react';
import { API_SAI_SO, API_MAC_DETAIL, API_HOP_DONG_DETAIL, API_SAI_SO_FILTER, API_EXPORT_EXCEL } from '../../constants/ApiConstant';
import AppUtil from '../../utils/AppUtil';
import Loading from '../../views/pages/Loading';
import DashBoardItem from './DashBoardItem';
import CIcon from '@coreui/icons-react';
import { Button, Modal } from 'antd';


const { RangePicker } = DatePicker;

const dateFormat = 'DD/MM/YYYY';

const layout = {
    labelCol: {
        span: 6,
    },
    wrapperCol: {
        span: 16,
    },
};
class Dashboard extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: [
                {
                    subTitle: 'Tổng chênh lệch (Tấn)',
                    results: [

                    ],
                    dataKeyMap: {
                        'da': 'Đá',
                        'catSong': 'Cát Sông',
                        'xiMang': 'Xi măng',
                        'troBay': 'Tro bay',
                        'nuoc': 'Nước',
                        'phuGia1': 'Phụ gia 1',
                        'phuGia2': 'Phụ gia 2'
                    },
                    dataColorMap: [
                        { key: "da", color: "#264653", name: 'da' },
                        { key: "catSong", color: "#2a9d8f", name: 'catSong' },
                        { key: "xiMang", color: "#e9c46a", name: 'xiMang' },
                        { key: "troBay", color: "#f4a261", name: 'troBay' },
                        { key: "nuoc", color: "blue", name: 'nuoc' },
                        { key: "phuGia1", color: "#e76f51", name: 'phuGia1' },
                        { key: "phuGia2", color: "#a8dadc", name: 'phuGia2' },


                    ],
                    unit: 'Tấn'
                },
                {
                    subTitle: 'Tổng chênh lệch (Mét khối)',
                    results: [

                    ],
                    dataKeyMap: {
                        'da_1m3': 'Đá',
                        'catSong_1m3': 'Cát',
                        'xiMang_1m3': 'Xi măng',
                        'phuGia1_1m3': 'Phụ gia 1',
                        'phuGia2_1m3': 'Phụ gia 2',
                    },
                    dataColorMap: [
                        { key: "da_1m3", color: "#264653", name: 'da_1m3' },
                        { key: "catSong_1m3", color: "#2a9d8f", name: 'catSong_1m3' },
                        { key: "xiMang_1m3", color: "#e9c46a", name: 'xiMang_1m3' },
                        { key: "phuGia1_1m3", color: "#e76f51", name: 'phuGia1' },
                        { key: "phuGia2_1m3", color: "#a8dadc", name: 'phuGia2' },
                    ],
                    unit: 'm3'
                }

            ],
            loading: true,
            year: moment().year(),
            startDate: moment(moment().format('DD/MM/YYYY'), 'DD/MM/YYYY').add(-1, 'Y').format('DD/MM/YYYY'),
            endDate: moment().format('DD/MM/YYYY'),
            typeFilter: 1
        }
        this.labelTooltip = "Chênh lệch"



    }

    getRangeDateMonth(fromDate, toDate) {
        const timekeys = [];
        const endDate = moment(toDate, 'DD/MM/YYYY').endOf('month');
        let currentDate = moment(fromDate, 'DD/MM/YYYY').startOf('month');
        while (currentDate <= endDate) {
            const obj = {
                label: currentDate.format('MM/YYYY'),
                end: currentDate.endOf('month').format('DD/MM/YYYY'),
                start: currentDate.startOf('month').format('DD/MM/YYYY')
            }
            timekeys.push(obj);
            currentDate.add(1, 'M');
        }
        return timekeys;
    }

    getRangeDate(fromDate, toDate) {
        const timekeys = [];
        const endDate = moment(toDate, 'DD/MM/YYYY');
        let currentDate = moment(fromDate, 'DD/MM/DYYYY');
        while (currentDate <= endDate) {
            const obj = {
                label: currentDate.format('DD/MM/YYYY'),
                // end: currentDate.endOf('days').format('DD/MM/YYYY'),
                // start: currentDate.startOf('days').format('DD/MM/YYYY')
            }
            timekeys.push(obj);
            currentDate.add(1, 'd');
        }
        return timekeys;
    }

    componentDidMount() {
        const requestMac = Axios.get(AppUtil.GLOBAL_API_PATH + API_MAC_DETAIL);
        const requestHopDong = Axios.get(AppUtil.GLOBAL_API_PATH + API_HOP_DONG_DETAIL);
        const requestSaiSo = Axios.get(AppUtil.GLOBAL_API_PATH + API_SAI_SO);
        Axios.all([requestMac, requestHopDong, requestSaiSo])
            .then(Axios.spread((...res) => {

                const responseMac = res[0];
                const responseHopDong = res[1];
                const responseSaiSo = res[2];

                if (res) {
                    // console.log("ThongTinMeTronListView -> componentDidMount -> data", data)
                    this.setState({
                        dataMac: responseMac.data.result,
                        dataHopDong: responseHopDong.data.result,
                        dataSaiSo: responseSaiSo.data.result
                    })
                    this.loadData(responseSaiSo.data.result, this.state.startDate, this.state.endDate, this.state.typeFilter)
                }

            }))
            .catch(() => {
                AppUtil.ToastError();
            })
            .finally(() => {
            });


    }

    onChangeMac(val) {
        this.loadingComponent && this.loadingComponent.onLoading()

        const dataPost = {
            startDate: this.state.startDate ? moment(this.state.startDate, 'DD/MM/YYYY').format('DD/MM/YYYY') : '',
            endDate: this.state.endDate ? moment(this.state.endDate, 'DD/MM/YYYY').format('DD/MM/YYYY') : '',
            macCode: val ? val : '',
            hopDongId: this.state.hopDongId ? this.state.hopDongId : ''
        }
        Axios.post(AppUtil.GLOBAL_API_PATH + API_SAI_SO_FILTER, dataPost)
            .then(res => {
                const { data } = res;
                if (data.success) {
                    this.loadData(data.result, this.state.startDate, this.state.endDate, this.state.typeFilter)

                    this.setState({
                        macCode: val
                    })
                } else {
                }
            })
            .catch(() => {
                AppUtil.ToastError();
            })
            .finally(() => {
                this.loadingComponent && this.loadingComponent.onStop()

            });
    }
    onChangeHopDong(val) {
        this.loadingComponent && this.loadingComponent.onLoading()

        const dataPost = {
            startDate: this.state.startDate ? moment(this.state.startDate, 'DD/MM/YYYY').format('DD/MM/YYYY') : '',
            endDate: this.state.endDate ? moment(this.state.endDate, 'DD/MM/YYYY').format('DD/MM/YYYY') : '',
            macCode: this.state.macCode ? this.state.macCode : '',
            hopDongId: val ? val : ''
        }
        Axios.post(AppUtil.GLOBAL_API_PATH + API_SAI_SO_FILTER, dataPost)
            .then(res => {
                const { data } = res;
                if (data.success) {
                    this.loadData(data.result, this.state.startDate, this.state.endDate, this.state.typeFilter)

                    this.setState({
                        hopDongId: val
                    })
                } else {
                    AppUtil.ToastError();

                }
            })
            .catch(() => {
                AppUtil.ToastError();
            })
            .finally(() => {
                this.loadingComponent && this.loadingComponent.onStop()

            });
    }

    onChangeDate(val) {
        this.loadingComponent && this.loadingComponent.onLoading()
        const dataPost = {
            startDate: val[0] ? val[0].format('DD/MM/YYYY').format('DD/MM/YYYY') : '',
            endDate: val[1] ? val[1].format('DD/MM/YYYY').format('DD/MM/YYYY') : '',
            macCode: this.state.macCode ? this.state.macCode : '',
            hopDongId: this.state.hopDongId ? this.state.hopDongId : ''
        }
        Axios.post(AppUtil.GLOBAL_API_PATH + API_SAI_SO_FILTER, dataPost)
            .then(res => {
                const { data } = res;
                if (data.success) {
                    this.loadData(data.result, moment(val[0]).format('DD/MM/YYYY'), moment(val[1]).format('DD/MM/YYYY'), this.state.typeFilter)

                    this.setState({
                        startDate: moment(val[0]).format('DD/MM/YYYY'),
                        endDate: moment(val[1]).format('DD/MM/YYYY')
                    })
                } else {
                    AppUtil.ToastError();
                }
            })
            .catch(() => {
                AppUtil.ToastError();
            })
            .finally(() => {
                this.loadingComponent && this.loadingComponent.onStop()

            });
    }

    onChangeFilterType(val) {
        this.loadingComponent && this.loadingComponent.onLoading()
        const dataPost = {
            startDate: this.state.startDate ? moment(this.state.startDate, 'DD/MM/YYYY').format('DD/MM/YYYY') : '',
            endDate: this.state.endDate ? moment(this.state.endDate, 'DD/MM/YYYY').format('DD/MM/YYYY') : '',
            macCode: this.state.macCode ? this.state.macCode : '',
            hopDongId: this.state.hopDongId ? this.state.hopDongId : ''
        }
        Axios.post(AppUtil.GLOBAL_API_PATH + API_SAI_SO_FILTER, dataPost)
            .then(res => {
                const { data } = res;
                if (data.success) {
                    this.loadData(data.result, this.state.startDate, this.state.endDate, val)
                    this.setState({
                        typeFilter: val
                    })
                } else {
                    AppUtil.ToastError();

                }
            })
            .catch(() => {
                AppUtil.ToastError();
            })
            .finally(() => {

                this.loadingComponent && this.loadingComponent.onStop()

            });
    }


    renderFilter() {
        const { dataMac, dataHopDong } = this.state
        return <Form  {...layout}
            name="basic"
            style={{ display: 'flex', background: '#fff', alignItems: 'center', borderRadius: 5, padding: 5, height: 60 }}

        >
            <Form.Item
                label="Loại"
                name="type"
                style={{ flex: 1, marginBottom: 0 }}

            >
                <Select
                    placeholder="Chọn"
                    // onChange={onGenderChange}
                    allowClear={false}
                    onChange={(val) => this.onChangeFilterType(val)}
                    defaultValue={1}

                >
                    <Select.Option value={1}>Theo tháng</Select.Option>
                    <Select.Option value={2}>Theo ngày</Select.Option>
                </Select>
            </Form.Item>
            <Form.Item
                label="Từ ngày"
                name="startDate"

                style={{ flex: 1, marginBottom: 0 }}
            >
                <RangePicker
                    defaultValue={[moment(this.state.startDate, dateFormat), moment(this.state.endDate, dateFormat)]}
                    format={dateFormat}
                    onChange={(val) => this.onChangeDate(val)}

                />
            </Form.Item>

            <Form.Item
                label="MAC"
                name="macCode"
                style={{ flex: 1, marginBottom: 0 }}

            >
                <Select
                    placeholder="Chọn"
                    // onChange={onGenderChange}
                    allowClear
                    onChange={(val) => this.onChangeMac(val)}

                >
                    {_.map(dataMac, item => {
                        return <Select.Option value={item.id} >
                            {item.macCode}
                        </Select.Option>
                    })}
                </Select>
            </Form.Item>
            <Form.Item
                label="Hợp đồng"
                name="hopDongId"
                style={{ flex: 1, marginBottom: 0 }}

            >
                <Select
                    placeholder="Chọn"
                    // onChange={onGenderChange}
                    allowClear
                    onChange={(val) => this.onChangeHopDong(val)}

                >
                    {_.map(dataHopDong, item => {
                        return <Select.Option value={item.id} >
                            {item.tenHopDong}
                        </Select.Option>
                    })}
                </Select>
            </Form.Item>
            <Form.Item
                // label={'Export excel'}
                name="export"
                style={{marginBottom: 0 }}

            >
                <Button tile='Export excel' style={{height: 32, marginBottom: 10}} key="submit" type="primary" size="large" onClick={this.exportExcel}>
                    <CIcon style={{ zIndex: 10, color: '#fff', fontSize: 12 }} name="cilDataTransferDown" />
                </Button>
            </Form.Item>
        </Form>
    }

    exportExcel = () => {
        window.open(AppUtil.GLOBAL_API_PATH + API_EXPORT_EXCEL)
        // Axios.get(AppUtil.GLOBAL_API_PATH + API_EXPORT_EXCEL)
        //     .then(res => {
        //         const { data } = res;
        //         // if (data.success) {
        //         //     AppUtil.ToastSuccess('Xuất excel thành công');
        //         // } else {
        //         //     AppUtil.ToastError();

        //         // }
        //     })
        //     .catch(() => {
        //         // AppUtil.ToastError();
        //     })
        //     .finally(() => {


        //     });
    }



    loadData(dataChart, startDate, endDate, typeFilter) {
        console.log('load data', startDate, endDate)
        const { data } = this.state;
        const dataChartTong = []
        const dataChartM3 = []
        // if (year = moment().year) {
        //     endDate = moment().format('DD/MM/YYYY')
        //     startDate = moment(year, 'DD/MM/YYYY').add(-1, 'Y').format('DD/MM/YYYY');
        // }

        let dataGroupBy = []

        let timekeys = []
        if (typeFilter == 2) {
            timekeys = this.getRangeDate(startDate, endDate);
            dataGroupBy = _.groupBy(dataChart, x => {
                return moment(_.get(x.thongTinMeTron, 'ngayTron')).format('DD/MM/YYYY')
            })
        } else {
            timekeys = this.getRangeDateMonth(startDate, endDate);
            dataGroupBy = _.groupBy(dataChart, x => {
                return moment(_.get(x.thongTinMeTron, 'ngayTron')).format('MM/YYYY')
            })
        }
        console.log(dataGroupBy, timekeys, startDate, endDate)

        _.forEach(timekeys, timekey => {
            Object.keys(dataGroupBy).forEach(month => {
                if (month == timekey.label) {
                    let da = 0
                    let catSong = 0
                    let xiMang = 0
                    let troBay = 0
                    let nuoc = 0
                    let phuGia1 = 0
                    let phuGia2 = 0
                    let da_1m3 = 0
                    let catSong_1m3 = 0
                    let xiMang_1m3 = 0
                    let phuGia1_1m3 = 0
                    let phuGia2_1m3 = 0
                    _.forEach(dataGroupBy[month], dta => {
                        da = da + dta.da
                        catSong = catSong + dta.catSong
                        xiMang = xiMang + dta.xiMang
                        troBay += dta.troBay
                        nuoc += dta.nuoc
                        phuGia1 += dta.phuGia1
                        phuGia2 += dta.phuGia2
                        da_1m3 += dta.da_1m3
                        catSong_1m3 += dta.catSong_1m3
                        xiMang_1m3 += dta.xiMang_1m3
                        phuGia1_1m3 += dta.phuGia1_1m3
                        phuGia2_1m3 += dta.phuGia2_1m3
                    })
                    const obj = {
                        name: month,
                        da: da.toFixed(2),
                        catSong: catSong.toFixed(2),
                        xiMang: xiMang.toFixed(2),
                        troBay: troBay.toFixed(2),
                        nuoc: nuoc.toFixed(2),
                        phuGia1: phuGia1.toFixed(2),
                        phuGia2: phuGia2.toFixed(2)
                    }
                    const _obj = {
                        name: month,
                        da_1m3: da_1m3.toFixed(2),
                        catSong_1m3: catSong_1m3.toFixed(2),
                        xiMang_1m3: xiMang_1m3.toFixed(2),
                        phuGia1_1m3: phuGia1_1m3.toFixed(2),
                        phuGia2_1m3: phuGia2_1m3.toFixed(2),
                    }
                    dataChartTong.push(obj)
                    dataChartM3.push(_obj)

                }
            })

        })
        data[0].results = dataChartTong
        data[1].results = dataChartM3
        this.setState({
            data,
            loading: false
        })
    }


    onDownload() {
        this.loadingComponent && this.loadingComponent.onLoading()
    }

    onDownloaded() {
        this.loadingComponent && this.loadingComponent.onStop()
    }
    render() {
        const { loading, data, maxData, minData } = this.state;
        if (loading) {
            return <Loading />
        }
        return (
            <div ref={component => this.Container = component} className={"container-haohut-lpg"} style={{ paddingLeft: 15, paddingRight: 15, height: "100%" }}>
                <LoadingComponent ref={c => { this.loadingComponent = c }} />
                {this.renderFilter()}
                {_.map(data, (element, i) => {
                    return (
                        <div key={i} style={{
                            width: '100%',
                            height: 350,
                            marginTop: 10
                        }} >
                            <DashBoardItem
                                title={this.title}
                                onDownload={() => this.onDownload(element.subTitle)}
                                onDownloaded={() => this.onDownloaded()}
                                dom={`image-dom${i}`}
                                headerColor={i % 3 === 0 ? "#D6F4B1" : i % 3 === 1 ? "#FDF2D5" : "#FFD7DA"}
                                exportIconColor={i % 3 === 0 ? "#298C39" : i % 3 === 1 ? "#FEC32A" : "#FB3042"}
                                dataKeyMap={data[i].dataKeyMap ? data[i].dataKeyMap : this.dataKeyMap}
                                dataColorMap={data[i].dataColorMap ? data[i].dataColorMap : this.dataColorMap}
                                subTitle={element.subTitle}
                                data={element.results}
                                range={[minData, maxData]}
                                ticks={[minData, maxData]}
                                unit={element.unit}
                                labelTooltip={this.labelTooltip}
                            />
                        </div>
                    )
                })}
            </div>

        )
    }
}

export default Dashboard;

class LoadingComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
        };
    }

    onLoading() {
        this.divElement && (this.divElement.style.display = "flex")
    }

    onStop() {
        this.divElement && (this.divElement.style.display = "none")
    }

    render() {
        return (
            <div ref={c => { this.divElement = c }} className="loading-component" style={{ zIndex: 99, display: "none", position: "absolute", top: 0, bottom: 0, left: 0, right: 0, backgroundColor: "#00000085", justifyContent: "center", alignItems: "center" }}>
                <div className="lds-ellipsis"><div></div><div></div><div></div><div></div></div>
            </div>
        );
    }
}
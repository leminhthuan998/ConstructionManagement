import React, { Component } from 'react';
import LineChartGroup from './LineChartGroup';
import DomToImage from 'dom-to-image';
import "./style/boxchart.scss";
import Loading from '../../views/pages/Loading';
import CIcon from '@coreui/icons-react';

class DashBoardItem extends Component {
    constructor(props) {
        super(props);
        this.state = {
            flag: false
        }
    }
    exportDom() {
        this.setState({ flag: true })
        this.props.onDownload && this.props.onDownload()
        const { title, subTitle, dom } = this.props;
        let titlee = title ? title : ""
        const domBody = document.getElementById(dom);
        DomToImage.toPng(domBody, { quality: 1, bgcolor: 'white' })
            .then(function (dataUrl) {
                const link = document.createElement("a");
                link.href = dataUrl;
                const _subTitle = subTitle.replace("/", "").replace("/", "")
                link.download = _subTitle.replace(/span|sub|<|>/gi, "")
                link.click();
                URL.revokeObjectURL(dataUrl);
            })
            .catch(function (error) {
                console.error('oops, something went wrong!', error);
            }).finally(() => {
                this.props.onDownloaded && this.props.onDownloaded()
            });
    }
    loading() {
        if (this.state.flag) {
            return (
                <Loading />
            )
        }
    }
    render() {
        const { subTitle, dom, headerColor, exportIconColor } = this.props;
        return (
            <div className='export-chart' style={{ height: "100%" }} >
                <div className="box-chart" id={dom}>
                    <div className="content-box-chart" style={{ backgroundColor: headerColor || "#eee", borderBottom: "2px solid", borderBottomColor: exportIconColor || "#ccc" }} >
                        <div className="title-box-chart">
                            <div className="sub-title" dangerouslySetInnerHTML={{ __html: subTitle }} ></div>
                        </div>
                        <button title="Xuất biểu đồ" type="button"
                            style={{ backgroundColor: exportIconColor || "#ccc" }}
                            className="btn btn-export button-export"
                            onClick={() => this.exportDom()}>
                            <CIcon style={{zIndex:10, color: '#fff', fontSize: 12}} name="cilDataTransferDown" />
                        </button>
                        
                    </div>
                    <LineChartGroup {...this.props} />
                </div>
            </div >
        );
    }
}

export default DashBoardItem;

DashBoardItem.defaultProps = {
    background: '#0093d9'
}


import React, { PureComponent } from 'react';
import {
  CartesianGrid,
  Legend,
  Line,
  LineChart,
  ReferenceLine,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis
} from 'recharts';
import './style/chart.scss';

class LineChartGroup extends PureComponent {
  render() {
    const { maxValue, minValue, dataKeyMap, dataColorMap, dataMaxMin, unit, labelTooltip = "", minWidth = 250 } = this.props;
    return (
      <ResponsiveContainer>
        {this.props.data.length > 0 ? (
          <LineChart data={this.props.data}
            margin={{ top: 5, right: 35, left: -20, bottom: 6 }}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis
              axisLine={false}
              tickLine={false}
              dataKey="name"
            />
            <YAxis
              axisLine={false}
              tickLine={false}
               />
            <Tooltip content={(props) => {
              const { payload, label } = props;
              return (
                <div className="hover-style-stack" style={{ fontSize: '13px', boxShadow: ` 2px 2px 8px 0 rgba(0, 0, 0, 0.4)  `, width: minWidth }}>
                  <div className="stack-title">{labelTooltip} {label}</div>
                  {
                    payload.map((entry, index) => {
                      if (dataKeyMap[entry.dataKey]) {
                        return (
                          // <div key={`item-${index}`}
                          //   style={{ display: 'flex', alignItems: 'center' }}
                          // >
                          //   <div style={{
                          //     height: 10,
                          //     width: 20,
                          //     background: entry.color,
                          //     marginRight: 5
                          //   }} />
                          //   {dataKeyMap[entry.dataKey]}: {entry.value}</div>
                          <div key={index} className="detail-style-stack">
                            <span className="dot-detail" style={{ backgroundColor: entry.color }}></span>
                            <span className="detail-title">
                              {dataKeyMap[entry.dataKey]}
                            </span>
                            <span className="detail-value">
                              {entry.value ? entry.value : "0"}

                              {/* {entry.value && entry.value.formatNumber() + ' '} */}
                              {/* <span style={{ paddingLeft: '5px' }} dangerouslySetInnerHTML={{ __html: unit ? unit : "" }}></span> */}
                              <span style={{ paddingLeft: '5px' }} dangerouslySetInnerHTML={{ __html: entry.payload[entry.name + '_unit'] ? entry.payload[entry.name + '_unit'] : unit ? unit : "" }}></span>
                            </span>
                          </div>

                        )

                      }
                      return null;
                    }
                    )
                  }
                </div>
              )
            }} />
            <Legend
              content={(props) => {
                const { payload } = props;
                return (
                  <div >
                    <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', flexWrap: 'wrap' }}>
                      {
                        payload.map((entry, index) => {
                          if (dataKeyMap[entry.dataKey]) {
                            return (<div key={`item-${index}`}
                              style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', marginRight: 10 }}
                            >
                              <div style={{
                                height: 10,
                                width: 10,
                                borderRadius: 5,
                                background: entry.color,
                                marginRight: 5,
                                fontSize: 10
                              }} />
                              <span style={{ fontSize: 12, color: "#000000" }}>
                                {dataKeyMap[entry.dataKey]}
                              </span>
                            </div>)
                          }
                          return null;
                        })
                      }
                    </div>
                  </div>
                )
              }
              }
            />
            {/* {
              maxValue ? <ReferenceLine className="cls-max-line" y={parseFloat(maxValue)} label="" strokeDasharray="4 2" stroke="#ff7f7d" /> : null
            }
            {
              minValue ? <ReferenceLine className="cls-min-line" y={parseFloat(minValue)} label="" stroke="#ff7f7d" /> : null
            } */}
            {dataColorMap.map((e, indexe) => {
              if (e.key.includes('Max') || e.key.includes('Min') || e.key.includes('dm_')) {
                return (<Line type="monotone" key={indexe} dataKey={e.key} stroke={e.color} dot={false} strokeDasharray={'5 5'} />)
              } else {
                return (<Line type="monotone" key={indexe} dataKey={e.key} stroke={e.color} />)
              }
            })}
          </LineChart>)
          : <div></div>}
      </ResponsiveContainer>
    );
  }
}

export default LineChartGroup;

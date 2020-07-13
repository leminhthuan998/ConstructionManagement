import { Transfer, Button } from 'antd';
import React, { Component } from 'react';
import _ from 'lodash'
class AddUserToRole extends Component {
    constructor(props) {
        super(props);
        this.state = {
            mockData: [],
            targetKeys: []
        }
    }


    componentDidMount() {
        this.getMock();
    }

    getMock = () => {
        const targetKeys = [];
        const mockData = [];
        const arr = this.props.dataUser;
        const roleUser = this.props.dataUserRole;
        // console.log("add to role -> getMock ", this.props.dataUserRole)
        // console.log("AddUserToRole -> getMock -> roleUser", roleUser)
        // console.log("AddUserToRole -> getMock -> arr", arr)
        for (let i = 0; i < _.size(roleUser); i++) {
            const data = {
                key: roleUser[i].id,
                userName: roleUser[i].userName
            };
            targetKeys.push(data);
        }
        // for (let i = 0; i < 20; i++) {
        //     const data = {
        //       key: i.toString(),
        //       title: `content${i + 1}`,
        //       description: `description of content${i + 1}`,
        //       chosen: Math.random() * 2 > 1,
        //     };
        //     if (data.chosen) {
        //       targetKeys.push(data.key);
        //     }
        //     // mockData.push(data);
        //   }
        console.log("AddUserToRole -> getMock -> targetKeys", targetKeys)


        for (let i = 0; i < arr.length; i++)
        {
            const data = {
                key: arr[i].id,
                userName: arr[i].userName
            }
            mockData.push(data)
        }
        this.setState({ mockData, targetKeys });
    };

    handleChange = targetKeys => {
        this.setState({ targetKeys });
    };

    renderFooter = () => (
        <Button size="small" style={{ float: 'right', margin: 5 }} onClick={this.getMock}>
            reload
        </Button>
    );

    render() {
        console.log("add to role -> ", this.props.dataUserRole)
        console.log("AddUserToRole -> render -> this.state.targetKeys", this.state.targetKeys)

        return (
            <Transfer
                dataSource={this.state.mockData}
                titles={['Users', 'Users in this Role']}
                showSearch
                listStyle={{
                    width: 250,
                    height: 300,
                }}
                operations={['to right', 'to left']}
                targetKeys={this.state.targetKeys}
                onChange={this.handleChange}
                render={item => `${item.userName}`}
            // footer={this.renderFooter}
            />
        );
    }
}

export default AddUserToRole;
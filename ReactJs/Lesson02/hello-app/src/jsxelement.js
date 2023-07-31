 const users ={
    
 }
 
 import React, { Component } from 'react'

export default class jsxelement extends Component {
 const users ={
    firstName:"Quoc",
    lastName:"Nguyen",
    age:45
 }
function formatname(user){
    return user.firstName + '' +user.lastName;
}

  const element = (
    <div> 
        <h1>Welcome</h1>
    </div>
  )
  render() {
    return (
      <div>jsxelement</div>
 )
  }
}

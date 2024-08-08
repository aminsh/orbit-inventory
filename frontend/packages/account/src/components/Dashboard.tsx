import { NavLink } from 'react-router-dom'

export const Dashboard = () => {
  return(
    <>
      <h1>Dashboard</h1>
      <NavLink to='/profile'>
        Profile
      </NavLink>
    </>
  )
}
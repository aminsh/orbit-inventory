import authenticatedUser from '../store/module/user/authenticatedUserApi'
import { useAppSelector } from '../store'

export const Dashboard = () => {
  const { data } = authenticatedUser.useFetchUserQuery()
  const { user } = useAppSelector(e => e.authenticatedUserState)

  return (
    <>
      <p>
        {data?.name} {data?.email}
      </p>
      <p>
        {JSON.stringify(user)}
      </p>
    </>
  )
}
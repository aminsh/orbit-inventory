import { useAppSelector } from '../store'

export const Dashboard = () => {
  const { user } = useAppSelector(e => e.authenticatedUserState)

  return (
    <>
      <p>
        {JSON.stringify(user)}
      </p>
    </>
  )
}
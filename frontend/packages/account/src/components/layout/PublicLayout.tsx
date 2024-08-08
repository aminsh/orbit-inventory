import { Outlet } from 'react-router-dom'

export const PublicLayout = () => {
  return (
    <>
      <div className='flex min-h-full flex-col justify-center px-6 py-12 lg:px-8'>
        <div className='mt-10 sm:mx-auto sm:w-full sm:max-w-sm'>
          <Outlet/>
        </div>
      </div>
    </>
  )
}

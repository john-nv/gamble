import AuthenLayout from '@components/AuthenLayout';
import ProtectedRoute from '@components/ProtectedRoute';
import LoadingScreen from '@components/ScreenInitial';
import '@scss/main.scss';
import BankAccount from '@views/BankAccount';
import CreateBankAccount from '@views/CreateBankAccount';
import Setting from '@views/Setting';
import Withdraw from '@views/Withdraw';
import React, { Suspense } from 'react';
import { HashRouter, Route, Routes } from 'react-router-dom';

const Home = React.lazy(() => import("@views/Home"))
const Login = React.lazy(() => import("@views/Login"))
const Registration = React.lazy(() => import("@views/Registration"))
const LuckySpaceship = React.lazy(() => import("@views/LuckySpaceship"))
const Account = React.lazy(() => import("@views/Account"))
const BettingHistory = React.lazy(() => import("@views/BettingHistory"))
const DepositHistory = React.lazy(() => import("@views/DepositHistory"))
const WithdrawHistory = React.lazy(() => import("@views/WithdrawHistory"))

const App = () => {
  return (
    <HashRouter>
      <Suspense fallback={<LoadingScreen />}>
        <Routes>
          <Route path="/" element={<ProtectedRoute><Home /></ProtectedRoute>} />
          <Route path="/tro-choi/lucky-spaceship" element={<ProtectedRoute><LuckySpaceship /></ProtectedRoute>} />
          <Route path="/dang-nhap" element={<AuthenLayout><Login /></AuthenLayout>} />
          <Route path="/dang-ky-tai-khoan" element={<AuthenLayout><Registration /></AuthenLayout>} />
          <Route path="/tai-khoan/thong-tin" element={<ProtectedRoute><Account /></ProtectedRoute>} />
          <Route path="/tai-khoan/lich-su-dat-cuoc" element={<ProtectedRoute><BettingHistory /></ProtectedRoute>} />
          <Route path="/tai-khoan/lich-su-nap-tien" element={<ProtectedRoute><DepositHistory /></ProtectedRoute>} />
          <Route path="/tai-khoan/lich-su-rut-tien" element={<ProtectedRoute><WithdrawHistory /></ProtectedRoute>} />
          <Route path="/tai-khoan/rut-tien" element={<ProtectedRoute><Withdraw /></ProtectedRoute>} />
          <Route path="/tai-khoan/cai-dat" element={<ProtectedRoute><Setting /></ProtectedRoute>} />
          <Route path="/tai-khoan/cai-dat/quan-ly-the-ngan-hang" element={<ProtectedRoute><BankAccount /></ProtectedRoute>} />
          <Route path="/tai-khoan/cai-dat/them-the-ngan-hang" element={<ProtectedRoute><CreateBankAccount /></ProtectedRoute>} />
        </Routes>
      </Suspense>
    </HashRouter >
  )
}

export default App

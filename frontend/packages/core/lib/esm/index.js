import * as React from 'react';
import { useState } from 'react';
import { useSearchParams, useNavigate, Outlet } from 'react-router-dom';
import { jsx, Fragment, jsxs } from 'react/jsx-runtime';
import { Alert, theme, Layout, Flex, Button, Avatar, Menu } from 'antd';
import { MenuOutlined } from '@ant-design/icons';

var HttpStatus;
(function (HttpStatus) {
    HttpStatus[HttpStatus["Success"] = 200] = "Success";
    HttpStatus[HttpStatus["NotFound"] = 404] = "NotFound";
    HttpStatus[HttpStatus["BadRequest"] = 400] = "BadRequest";
    HttpStatus[HttpStatus["Unauthorized"] = 401] = "Unauthorized";
    HttpStatus[HttpStatus["Unknown"] = 500] = "Unknown";
})(HttpStatus || (HttpStatus = {}));

const say = (word) => word + 'by me';

const configure = (config) => {
    configuration = config;
};
let configuration;

const translate = (...keys) => {
    return keys.map(resolve).join(' ');
};
const setDefaultDictionary = (name) => {
    configuration.defaultDictionary = name;
};
const resolve = (key) => {
    const value = configuration.dictionaries[configuration.defaultDictionary][key];
    return value || key;
};

/******************************************************************************
Copyright (c) Microsoft Corporation.

Permission to use, copy, modify, and/or distribute this software for any
purpose with or without fee is hereby granted.

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH
REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY
AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT,
INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM
LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR
OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR
PERFORMANCE OF THIS SOFTWARE.
***************************************************************************** */
/* global Reflect, Promise, SuppressedError, Symbol */


function __awaiter(thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
}

typeof SuppressedError === "function" ? SuppressedError : function (error, suppressed, message) {
    var e = new Error(message);
    return e.name = "SuppressedError", e.error = error, e.suppressed = suppressed, e;
};

const httpRequest = (args) => __awaiter(void 0, void 0, void 0, function* () {
    const result = yield fetch(`${configuration.baseUrl}/v1/${args.url}`, {
        headers: {
            'Content-Type': 'application/json',
        },
        method: args.method,
        body: JSON.stringify(args.body),
    });
    const response = {
        status: result.status,
        response: null,
        error: null,
    };
    try {
        response[response.status === HttpStatus.Success ? 'response' : 'error'] = yield result.json();
        return response;
    }
    catch (e) {
        return response;
    }
});

const JSON_START = /^\[|^\{(?!\{)/;
const JSON_ENDS = {
    '[': /]$/,
    '{': /}$/
};
const memory = {
    set: (key, value) => {
        const correctedValue = typeof value === 'object'
            ? JSON.stringify(value)
            : value;
        localStorage.setItem(key, correctedValue);
    },
    get: (key) => {
        const result = localStorage.getItem(key);
        if (!result)
            return null;
        return isJsonLike(result)
            ? JSON.parse(result)
            : result;
    },
    remove: (key) => {
        localStorage.removeItem(key);
    }
};
const isJsonLike = (value) => {
    const jsonStart = value.match(JSON_START);
    return jsonStart && JSON_ENDS[jsonStart[0]].test(value);
};

const useHttpRequest = (args) => {
    const [loading, setLoading] = useState(false);
    const execute = (overrideArgs) => __awaiter(void 0, void 0, void 0, function* () {
        setLoading(true);
        const response = yield httpRequest(Object.assign(Object.assign({}, args), overrideArgs));
        setLoading(false);
        return response;
    });
    return [execute, loading];
};

const useMemory = () => memory;

const AUTHENTICATION_TOKEN = 'authentication_token';
const CALLBACK_URL = 'callbackUrl';
const CALLBACK = 'callback';
const ACCESS_TOKEN = 'access_token';
const TOKEN_TYPE = 'token_type';
const STATE = 'state';
const DEFAULT_PATH = '/';

const useAuth = () => {
    const memory = useMemory();
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    const getToken = () => memory.get(AUTHENTICATION_TOKEN);
    const saveToken = (token) => memory.set(AUTHENTICATION_TOKEN, token);
    return {
        saveToken: (token) => memory.set(AUTHENTICATION_TOKEN, token),
        check: () => {
            const token = getToken();
            if (token) {
                configuration.token = token;
                return;
            }
            const url = new URL(configuration.authUrl);
            const state = {
                originalPath: location.pathname,
            };
            url.searchParams.append(CALLBACK_URL, `${location.origin}/${CALLBACK}`);
            url.searchParams.append(STATE, encodeURIComponent(JSON.stringify(state)));
            location.href = url.toString();
        },
        callback: () => {
            var _a;
            const accessToken = searchParams.get(ACCESS_TOKEN);
            const tokenType = searchParams.get(TOKEN_TYPE);
            if (!accessToken)
                return;
            if (!tokenType)
                return;
            saveToken({
                accessToken,
                tokenType,
            });
            const encodedState = searchParams.get(STATE);
            const state = encodedState
                ? JSON.parse(decodeURIComponent(encodedState))
                : {};
            const destinationPath = (_a = state.originalPath) !== null && _a !== void 0 ? _a : DEFAULT_PATH;
            navigate(destinationPath);
        },
    };
};

const ErrorMessage = ({ message, title, onClose }) => {
    const canShow = () => {
        if (!message)
            return false;
        return !(Array.isArray(message) && message.length === 0);
    };
    return (jsx(Fragment, { children: canShow() &&
            jsx(Alert, { className: 'mb-2', message: title, description: Array.isArray(message) ?
                    message.length > 1 ?
                        jsx("ul", { children: message.map(msg => jsx("li", { children: msg })) })
                        : message[0]
                    : message, type: "error", showIcon: true, closable: true, onClose: onClose }) }));
};

var _g;
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
var SvgOrbit = function SvgOrbit(props) {
  return /*#__PURE__*/React.createElement("svg", _extends({
    xmlns: "http://www.w3.org/2000/svg",
    width: "1247.000000ptpx",
    height: "1247.000000ptpx",
    viewBox: "0 0 1247 1247"
  }, props), _g || (_g = /*#__PURE__*/React.createElement("g", {
    fill: "#123aea"
  }, /*#__PURE__*/React.createElement("path", {
    d: "M668 240.1c-78.5 6.8-154.7 36.2-222.2 85.7-4.9 3.5-8.8 6.6-8.8 6.8 0 .3 2.6-1.2 5.8-3.3 46.9-30.4 103.8-51.4 159.2-58.8 16.7-2.3 52.5-3.1 68.7-1.6 55.4 5.1 102 22 143 51.8 29.8 21.7 58.8 55.5 74.7 87 3.9 7.8 4.3 9.5 2.4 8.8-2.2-.7-40.6-5.4-61.3-7.5-106.8-10.5-228.1-10-341.5 1.5-90.2 9.2-177.3 25.3-249.5 46.1-21.1 6.1-62.7 19.8-65.3 21.5-1.6 1-1.6 1 .1.5 3.9-1.2 47.5-9.7 66.7-13 52.4-9 106.2-15.7 161.5-20.1 82.6-6.5 187.9-7.3 268-2 33.7 2.3 89.1 7.8 114 11.5 34.7 5 80.6 14.1 112 22 110.8 28 182.4 68.5 204.1 115.5 8.7 18.9 8.6 40-.5 60-13.8 30.3-51.2 62.9-100.8 87.9-4 2-7.3 3.8-7.3 4.1 0 .2 5.3-1.4 11.8-3.6 118.2-40.8 188.4-92.6 198.8-146.7 8.1-42.5-24-83.4-91.9-116.8-29-14.3-60.7-25.9-100-36.7l-18.9-5.2-3.4-9c-5.3-13.9-17.5-37.7-25.9-50.9-20.6-32.2-48.1-60.6-80-82.8-42.6-29.6-96.2-48-154-52.8-13.9-1.1-45.7-1.1-59.5.1"
  }), /*#__PURE__*/React.createElement("path", {
    d: "M918.6 512.7c1.3 14.2.7 45.7-1.1 60.8-4.1 35.1-13.1 69-27.1 101.9-47.7 112-150.9 202.5-267.8 234.5-107.5 29.5-214.4 7.6-284.5-58.2-5.7-5.3-7.6-6.6-8.3-5.6-.7 1.1-.8 1.1-.6-.2.4-1.9-12.2-16-13.5-15.2-.6.3-.7.1-.3-.5.3-.6-.8-2.6-2.6-4.4s-3.2-3.5-3.3-3.8c0-.3-.4-.4-.8-.2-.5.1-.5-.2-.2-.8.4-.7-.2-2.1-1.3-3.3-3.5-3.9-14.3-20.9-20.1-31.8-7.8-14.3-13.4-27.3-18.6-43.1-2.4-7.3-4.4-12.8-4.5-12.1 0 2.4 5 24.2 7.9 33.8 5.5 18.3 12.5 36.4 19.8 51 5.2 10.4 14.6 25.4 15.6 24.8.6-.3.7-.1.3.5-.4.7 1.2 3.9 3.6 7.3s5 7.1 5.8 8.3c9.6 13.8 33.3 39.1 47.6 50.7 45.6 37.1 98.2 59.2 160.9 67.6 18.8 2.5 67.6 2.5 87 0 50.2-6.5 93.2-19.3 137.9-41.1 84.9-41.5 157.1-109.4 204.1-192.1 19.4-34 35.3-74.6 43.9-111.5 6.2-27 9-48 10.2-77.1l.7-16.5-10.4-4.6c-13.1-5.8-21.1-8.9-36.8-14.5-13.5-4.7-41.2-13.3-43.1-13.3-.9 0-1 2.2-.4 8.7M107.6 508.5c-24.1 5-44 15.8-61.7 33.4-15.4 15.3-24.7 31.4-30 51.7-3.3 12.6-3.3 32.4-.1 43.9 7 24.2 24.8 42.5 49.2 50.3 11 3.5 18 4.5 31 4.4 31.9-.2 61-12.9 84.1-36.9q25.8-26.7 30-62.7c4.5-38.3-17-71.2-53.9-82.2-7.6-2.3-10.8-2.7-25.2-3-11.5-.2-18.6.1-23.4 1.1m24.4 53.6c9.2 4.1 15.2 12.3 17.2 23.6 2.3 13.5-2.7 28.4-13.4 39.9-18.9 20.2-48.4 18.4-58.7-3.6-2.2-4.7-2.6-6.8-2.5-15 0-8 .5-10.6 2.9-16.5 3.5-8.8 6.8-13.7 13.4-19.8 11.5-10.7 28.2-14.2 41.1-8.6"
  }), /*#__PURE__*/React.createElement("path", {
    d: "M241.6 510.9c-.5.8-6.1 31.2-14.1 77.1-1.9 10.7-6.6 37.5-10.5 59.4-3.8 21.9-7 40.3-7 40.8 0 .4 13.4.7 29.8.6l29.7-.3 4.4-25.3 4.5-25.2h12.7l12.4 25.5 12.4 25.5h68.2l-15.5-30.4-15.6-30.4 5.3-2.7c21.6-11.3 34.4-27.9 39.1-50.7 5.4-26.1-3.5-46.2-24.8-56.2-8.8-4.2-17.8-6.4-30.9-7.6-12.4-1.2-99.4-1.3-100.1-.1m88.9 51.8c5.7 3 7.9 7.2 7.1 13.7-.8 6.7-4.7 12.3-11.1 15.5-4.7 2.5-5.9 2.6-22.8 2.9l-17.9.3.6-2.8c1-4.8 5.6-31.1 5.6-31.8 0-.4 7.8-.5 17.3-.3 15.1.4 17.7.7 21.2 2.5M423.6 510.8c-.3.5-4.6 24-9.6 52.3-15.2 86.5-19 108.1-20.6 116.3-.7 4.3-1.4 8.2-1.4 8.7 0 1.6 102.9 1.1 115-.5 30.7-4.2 51.4-18.6 59.3-41.4 1.4-4 2-8.6 2.1-15.2.1-8-.3-10.3-2.3-14.8-3.2-7.1-11.4-14.4-20.2-17.8l-6.8-2.7 9.1-4.5c15.6-7.8 24.6-18.9 28.4-34.9 5.1-21.7-6.6-38.1-31.3-44-5.7-1.4-15.7-1.7-63.9-2-34.7-.3-57.4-.1-57.8.5m85.6 46.1c6.8 2.6 8.8 9.3 4.8 15.9-3.7 6-8.1 7.2-27.8 7.2-16.1 0-17.3-.1-16.8-1.8.3-.9 1.3-6.2 2.2-11.7s2.2-10.3 2.8-10.8c1.6-1.1 31.6-.1 34.8 1.2m-8.8 61.2c2.2.6 5.3 2.4 6.9 4 2.6 2.6 2.9 3.5 2.4 7.1-.7 5.5-3.3 8.8-9.2 11.8-4.5 2.2-6.3 2.4-22.8 2.8-10 .3-18.2.1-18.8-.5-.7-.7 2.1-19.9 3.6-25.1.6-1.6 32.2-1.7 37.9-.1M604 510.7c0 .5-1.8 10.7-4 22.8-7.6 42.3-27 153.1-27 154.3 0 .9 7.1 1.2 29.7 1l29.7-.3 3.8-21.5c7.4-41.7 11.9-67.7 15.9-90.5 2.2-12.7 4.4-25.3 4.9-28 5.5-30.7 6.6-37.6 6.2-38-.8-.8-59.2-.6-59.2.2M677.5 511.4c-.3 1-.8 4.1-3.6 21.6-.6 3.6-1.7 9.4-2.5 13-.7 3.6-1.6 8.4-1.9 10.7l-.7 4.3h26.1c14.4 0 26.1.2 26.1.5 0 .6-5.5 32-14.5 82.6-4.1 23.4-7.5 43.1-7.5 43.8 0 .8 7.8 1.1 29.7.9l29.7-.3 10.7-60.5c5.8-33.3 10.9-62 11.3-63.7l.8-3.3 26.1-.2 26.2-.3 4.2-23.5c2.3-12.9 4.2-24.3 4.2-25.3.1-1.7-3.5-1.8-82-1.6-57.3.2-82.2.6-82.4 1.3"
  }))));
};

const headerStyle = {
    padding: '0 20px',
    height: '6%',
    borderBlockEnd: '1px solid rgba(5, 5, 5, 0.06)'
};
const { Header, Sider, Content } = Layout;
const MainLayout = ({ menuItems }) => {
    const { token } = theme.useToken();
    const [collapsed, setCollapsed] = useState(false);
    const toggle = () => setCollapsed(!collapsed);
    return jsxs(Layout, { style: { height: '100vh' }, children: [jsx(Header, { style: Object.assign(Object.assign({}, headerStyle), { backgroundColor: token.colorBgContainer }), children: jsxs(Flex, { align: 'center', style: { height: '100%' }, children: [jsxs(Flex, { align: 'center', justify: 'start', style: { width: '100%' }, children: [jsx(Flex, { align: 'center', justify: 'start', style: { width: 180 }, children: jsx(SvgOrbit, { style: { width: '60px', maxHeight: '70px' } }) }), jsx(Button, { onClick: toggle, style: { color: 'blue' }, type: 'text', shape: 'circle', icon: jsx(MenuOutlined, {}) })] }), jsx(Flex, { align: 'center', justify: 'end', style: { width: '100%' }, children: jsx(Avatar, { style: { backgroundColor: '#8795de' }, children: "K" }) })] }) }), jsxs(Layout, { children: [jsx(Sider, { width: '13%', collapsed: collapsed, children: jsx(Menu, { style: { height: '100%' }, defaultSelectedKeys: ['1'], defaultOpenKeys: ['sub1'], mode: 'inline', items: menuItems }) }), jsx(Content, { children: jsx(Outlet, {}) })] })] });
};

export { ErrorMessage, HttpStatus, MainLayout, configuration, configure, httpRequest, memory, say, setDefaultDictionary, translate, useAuth, useHttpRequest, useMemory };

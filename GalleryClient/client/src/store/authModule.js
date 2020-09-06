const auth = {
    namespaced: true,
    state: {
        token: localStorage.getItem('token'),
        username: localStorage.getItem('username')
    }
}

export default auth;
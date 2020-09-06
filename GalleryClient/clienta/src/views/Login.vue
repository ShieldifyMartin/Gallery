<template>
  <div class="login">
    <h1>Login</h1>
    <form method="post" @submit.prevent="handleSubmit">
      <input type="text" v-model="email" name="email" placeholder="Email" />
      <input type="text" v-model="username" name="username" placeholder="Username" />
      <input type="password" v-model="password" name="password" placeholder="Password" />
      <input type="submit" value="Send" class="submit-btn" />
    </form>
    <router-link to="/register">Don't have an account</router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "Login",
  data() {
    return {
      email: "",
      username: "",
      password: "",
    };
  },
  methods: {
    handleSubmit() {
      const { email, username, password } = this;
      console.log({ email, username, password });
      //const { dispatch } = this.$store;
      //if (username && password) {
      //  dispatch("authentication/login", { username, password });
      //}
    },
  },
});
</script>

<style lang="scss">
.login {
  * {
    display: block;
    margin: 1em auto;
  }

  input {
    width: 30em;
    height: 2em;
  }

  .submit-btn {
    border: none;
    width: 30em;
    background: #111111;
    color: #ffff;
  }

  a {
    text-decoration: none;
    color: black;
  }

  a:hover {
    color: rgba(0, 0, 0, 0.467);
  }
}
</style>
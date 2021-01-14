<template>
  <div class="login">
    <h1>Login</h1>
    <form method="post" @submit.prevent="handleSubmit">
      <input
        type="text"
        v-model="state.username"
        name="username"
        placeholder="Username"
        required
      />
      <input
        type="password"
        v-model="state.password"
        name="password"
        placeholder="Password"
        required
      />
      <input type="submit" value="Send" class="submit-btn" />
    </form>
    <router-link to="/register">Don't have an account</router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue";
import Swal from "sweetalert2";
import store from "../../store";
import router from "../../router";
import { userService } from "../../services";

export default defineComponent({
  name: "Login",
  setup() {
    const state = reactive({
      username: "",
      password: "",
      isMobile: screen.width <= 700
    });

    async function handleSubmit() {
      const { username, password } = state;

      const status = await userService.login(
        store.state.auth.state.token,
        username,
        password
      );

      if (status === 200) {
        store.state.auth.dispatch("login");
        router.push("/");
      } else {
        Swal.fire({
          position: state.isMobile ? "top" : "top-end",
          icon: "error",
          title: "Something went wrong!",
          showConfirmButton: false,
          width: state.isMobile ? 250 : 300,
        });
        state.password = "";
      }
    }

    return {
      state,
      handleSubmit,
    };
  },
});
</script>

<styles src="./index.scss"></styles>

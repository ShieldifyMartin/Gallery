<template>
  <div class="register">
    <h1>Register</h1>
    <p class="error">{{ state.error }}</p>
    <form method="post" @submit.prevent="handleSubmit">
      <input
        type="email"
        v-model="state.email"
        name="email"
        placeholder="Email"
        required
      />
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
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue";
import Swal from "sweetalert2";
import store from "../../store";
import router from "../../router";
import validate from "./validator";
import { userService } from "../../services";

export default defineComponent({
  name: "Register",
  setup() {
    const state = reactive({
      email: "",
      username: "",
      password: "",
      isMobile: screen.width <= 700,
    });

    const handleSubmit = async () => {
      const { email, username, password } = state;
      const isCorrect = validate(state);

      if (!isCorrect) {
        return;
      }

      var message = await userService.register(
        store.state.auth.state.token,
        email,
        username,
        password
      );
            
      if (message !== null) {
        Swal.fire({
          position: state.isMobile ? "top" : "top-end",
          icon: "error",
          title: message,
          showConfirmButton: false,
          timer: 1500,
          width: state.isMobile ? 250 : 300,
        });
      } else {
        Swal.fire({
          position: state.isMobile ? "top" : "top-end",
          icon: "success",
          title: "Successful registration!",
          showConfirmButton: false,
          timer: 1500,
          width: state.isMobile ? 250 : 300,
        });
  
        router.push("/login");
      }
    };

    return {
      state,
      handleSubmit,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
